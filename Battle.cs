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
		customSignals.OnDialogPass += continueBattle;
		customSignals.OnBattleMove += MoveConfirmed;  
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
		menu_de_pelea.receiveLists(enemieslist, allylist);
		Fighter fig = turnManager.inAction();
		fig.passData().isMyTurn();
		while(fig.passData().ControlPlayer == false){
			turnManager.passTurnToNextFighter();
			fig = turnManager.inAction();
		}
		menu_de_pelea.makeMenuVisible(fig);
	}
	
	public void analizeBattle(){
		turnManager.passTurnToNextFighter();
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
	
	public void MoveConfirmed(Fighter actor, Movimiento mov_actual){
		movimientoUsado = mov_actual;
	}
	
	public async void continueBattle(){
		movimientoUsado.efecto();
		soundplayer.Play();
		await ToSignal(customSignals, "OnMoveResolved");
		customSignals.OnDialogPass -= continueBattle;
		while(movimientoUsado.someAffected()){
			customSignals.EmitSignal(nameof(CustomSignals.OnBattleEffectDialogRequested));
			await ToSignal(customSignals, "OnDialogPass");
		}
		customSignals.OnDialogPass += continueBattle;
		movimientoUsado.erraseTarget();
		analizeBattle();
	}
}
