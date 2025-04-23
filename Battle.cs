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
	
	//Managers
	private TurnManager turnManager;
	
	//Equipos y listas
	List<Fighter> allylist;
	List<Fighter> enemieslist;
	
	private Movimiento movimientoUsado;
	
	
	public override void _Ready(){
		customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		enfrentamiento = GetNode<Enfrentamiento>("EnfrentamientoAletorio");
		allies = GetNode<FighterTeam>("Equipo_Aliado");
		menu_de_pelea = GetNode<MenuBatalla>("Menu_Batalla");
		dialog = GetNode<CuadroTexto>("Dialogo");
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
		string dialogo = actor.passData().Name  + " ha usado " + movimientoUsado.giveTitulo() + " en " ; 
		dialogo  = $"{dialogo}{movimientoUsado.objetivos[0].passData().Name}";
		if( movimientoUsado.objetivos.Count > 1){
			int i = 1;
			while(i <  movimientoUsado.objetivos.Count){
				dialogo = $"{dialogo}, {movimientoUsado.objetivos[i].passData().Name}";
				i++;
			}
			dialogo += $"{dialogo} y {movimientoUsado.objetivos[i].passData().Name}";
		}
		GD.Print("Dialogo preparado.");
		dialog.ShowDialog(dialogo);
		//customSignals.EmitSignal(nameof(CustomSignals.OnDialogRequested), dialogo);
	}
}
