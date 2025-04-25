using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Battle : Node2D
{
	private CustomSignals customSignals;
	
	//Nodos
	private Enfrentamiento enfrentamiento;
	private FighterTeam allies;
	private MenuBatalla menu_de_pelea;
	private CuadroTexto dialog;
	private AudioStreamPlayer2D soundplayer;
	
	int fase = 0;
	
	//Managers
	private TurnManager turnManager;
	
	//Equipos y listas
	List<Fighter> allylist;
	List<Fighter> enemieslist;
	
	private Movimiento movimientoUsado;
		
	public override void _Ready(){
		customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		customSignals.OnDialogConfirmed += continueBattle;
		enfrentamiento = GetNode<Enfrentamiento>("EnfrentamientoAletorio");
		allies = GetNode<FighterTeam>("Equipo_Aliado");
		menu_de_pelea = GetNode<MenuBatalla>("Menu_Batalla");
		dialog = GetNode<CuadroTexto>("Dialogo");
		soundplayer = GetNode<AudioStreamPlayer2D>("SoundPlayer2D");
		allylist = allies.giveList();
		if(allylist == null){
			GD.PrintErr("allyList no tiene lista.");
		}
		enemieslist = enfrentamiento.giveList();
		if(enemieslist == null){
			GD.PrintErr("enemiesList no tiene lista.");
		}
		
		GD.Print("<Printing Entity NAMES/LEVEL>");
		GD.Print("--PLAYER'S TEAM--");
		foreach (Fighter f in allylist){
			Entity c = f.passData();
			GD.Print("NAME: " + c.Name);
			GD.Print("LEVEL: " + c.Level);
			GD.Print("*****************************");
		}
		GD.Print("---------------------------");
		GD.Print("--ENEMIES'S TEAM--");
		foreach (Fighter f in enemieslist){
			Entity c = f.passData();
			GD.Print("NAME: " + c.Name);
			GD.Print("LEVEL: " + c.Level);
			GD.Print("*****************************");
		}
		
		turnManager = new TurnManager(allylist, enemieslist);
		menu_de_pelea.receiveLists(enemieslist, allylist, this);
		this.analizeBattle();
	}
	
	public void analizeBattle(){
		Fighter fig = turnManager.inAction();
		fig.passData().isMyTurn();
		while(fig.passData().ControlPlayer == false){
			turnManager.passTurnToNextFighter();
			fig = turnManager.inAction();
		}
		menu_de_pelea.makeMenuVisible(fig);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void prepareDialog(Fighter actor, Movimiento mov_actual){
		movimientoUsado = mov_actual;
		string dialogo = actor.passData().Name  + " ha usado " + movimientoUsado.giveTitulo(); 
		if(movimientoUsado.whoAffects() != 3){
			dialogo  = $"{dialogo} en ";
			if(movimientoUsado.affectsAllTeam()){
				if(movimientoUsado.whoAffects() == 0)
					dialogo  = $"{dialogo}su equipo";
				else if(movimientoUsado.whoAffects() == 1)
					dialogo  = $"{dialogo}el equipo enemigo";
			}else{
				dialogo  = $"{dialogo}{movimientoUsado.objetivos[0].passData().Name}";
				if(movimientoUsado.objetivos.Count > 1 ){
					int i = 1;
					while(i <  movimientoUsado.objetivos.Count-1){
						dialogo = $"{dialogo}, {movimientoUsado.objetivos[i].passData().Name}";
						i++;
					}
					dialogo = $"{dialogo} y {movimientoUsado.objetivos[i].passData().Name}";
				}
			}
		}
		dialogo = $"{dialogo}.";
		GD.Print($"Dialogo preparado: {dialogo}");
		dialog.ShowDialog(dialogo);
	}
	public void prepareDialogConsecuence(){
		string dialogo = "";
		string affected = "";
		if(movimientoUsado.someAffected()){
			var entry = movimientoUsado.afectados.First();
			affected = entry.Key; 
			dialogo  = $"{affected} ha sido afectado por";
			List<Estado> estados = entry.Value;
			int i = 0;
			dialogo = $"{dialogo} {EstadosATexto(estados[i])}";
			i++;
			if(i <  estados.Count){
				while(i <  estados.Count-1){
					dialogo = $"{dialogo}, {EstadosATexto(estados[i])}";
					i++;
				}
				dialogo = $"{dialogo} y {EstadosATexto(estados[i])}";
			}
		}
		dialogo = $"{dialogo}.";
		GD.Print($"Dialogo preparado: {dialogo}");
		movimientoUsado.removeAffected(affected);
		dialog.ShowDialog(dialogo);
		//customSignals.EmitSignal(nameof(CustomSignals.OnDialogRequested), dialogo);
	}
	
	
	public async void continueBattle(){
		movimientoUsado.efecto();
		soundplayer.Play();
		await ToSignal(customSignals, "OnMoveResolved");
		customSignals.OnDialogConfirmed -= continueBattle;
		while(movimientoUsado.someAffected()){
			prepareDialogConsecuence();
			await ToSignal(customSignals, "OnDialogConfirmed");
		}
		customSignals.OnDialogConfirmed += continueBattle;
		movimientoUsado.erraseTarget();
		analizeBattle();
	}
	
	public string EstadosATexto(Estado e){
		switch(e){
			case Estado.BuffDMG:
				return "un potenciador de daño";
			case Estado.DeBuffDMG:
				return "una reduccion del de daño";
			case Estado.BuffDEF:
				return "un potenciador de defensa";
			case Estado.DeBuffDEF:
				return "una reduccion del de defensa";
			case Estado.BuffVEL:
				return "un potenciador de velocidad";
			case Estado.DeBuffVEL:
				return "una reduccion del de velocidad";
			case Estado.Aturdido:
				return "aturdimiento";
			case Estado.Sellado:
				return "sello magico";
			case Estado.Bloqueo:
				return "bloqueo de defensas";
			case Estado.Sangrado:
				return "sangrado";
			case Estado.Envenenado:
				return "veneno";
			case Estado.Regeneracion:
				return "regeneracion pasiva de vida";
			case Estado.Energetico:
				return "regeneracion pasiva de maná";
			case Estado.Evasion:
				return "evasión";
			case Estado.Marca_del_cazador:
				return "la marca del cazador";
			case Estado.Creacion:
				return "el potenciador de Alex";
			case Estado.Vanguardia:
				return "la proteccion de Vyls";
			default:
				return "un estado no reconocido (espera, que?)";
		}
	}
}
