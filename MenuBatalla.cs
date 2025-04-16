using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MenuBatalla : Control
{
	private Panel actingMenu;
	private Button attack;
	private Button special;
	private Button bag;
	private Button guard;
	private Panel specialMenu;
	private Button mov1;
	private Button mov2;
	private Button mov3;
	private Button mov4;
	private Panel selecting;
	private Button inv;

	List<Fighter> allylist;
	List<Fighter> enemieslist;
	
	Fighter actor;
	
	private bool selectingTarget;
	private Flecha flecha; // Nodo de la flecha
	private int currentTargetIndex = 0; // Índice del enemigo seleccionado
	
	private int num_selec = 0;
	private bool[] indexes = {false,false,false,false};
	Movimiento mov_actual;
	
	public override void _Ready()
	{
		attack = GetNode<Button>("Battle_Action/MarginContainer/HBoxContainer/Attack");
		special = GetNode<Button>("Battle_Action/MarginContainer/HBoxContainer/Special");
		bag = GetNode<Button>("Battle_Action/MarginContainer/HBoxContainer/Bag");
		guard = GetNode<Button>("Battle_Action/MarginContainer/HBoxContainer/Guard");
	   
		actingMenu = GetNode<Panel>("Battle_Action");
		specialMenu = GetNode<Panel>("Special_Action");
		selecting = GetNode<Panel>("Selection");
		
		mov1 = GetNode<Button>("Special_Action/MarginContainer/HBoxContainer/Mov1");
		mov2 = GetNode<Button>("Special_Action/MarginContainer/HBoxContainer/Mov2");
		mov3 = GetNode<Button>("Special_Action/MarginContainer/HBoxContainer/Mov3");
		mov4 = GetNode<Button>("Special_Action/MarginContainer/HBoxContainer/Mov4");
		
		inv = GetNode<Button>("Selection/MarginContainer/HBoxContainer/Inv");
		
		flecha = GetNode<Flecha>("Flecha");
		flecha.ShowArrow(false);
		
		attack.Pressed += OnAttackButtonDown;
		special.Pressed += OnSpecialButtonDown;
		bag.Pressed += OnBagButtonDown;
		guard.Pressed += OnGuardButtonDown;
		
		mov1.Pressed += OnMov1ButtonDown;
		mov2.Pressed += OnMov2ButtonDown;
		mov3.Pressed += OnMov3ButtonDown;
		mov4.Pressed += OnMov4ButtonDown;
		
		actingMenu.Visible = false;
		specialMenu.Visible = false;
		selecting.Visible = false;
		
		selectingTarget = false;
	}
	
	public void makeMenuVisible(Fighter f){
		GD.Print("MakingMenuVisible...");
		this.prepareTitles(f);
		this.ChangeMenu(0);
	}
	
	public void prepareTitles(Fighter f){
		Entity dataf = f.passData();
		attack.Text = dataf.atqBasico.giveTitulo();
		mov1.Text = dataf.mov1.giveTitulo();
		mov2.Text = dataf.mov2.giveTitulo();
		if(dataf.mov3.moveIsAvailable()){
			mov3.Text = dataf.mov3.giveTitulo();
		}else{
			mov3.Text = "Nivel 2";
			mov3.Disabled = true;
		}
		if(dataf.mov4.moveIsAvailable()){
			mov4.Text = dataf.mov4.giveTitulo();
		}else{
			mov4.Text = "Nivel 3";
			mov4.Disabled = true;
		}
		actor = f;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_cancel"))
		{
			ChangeMenu(0);
		}
		if (this.selectingTarget)
		{
			// Cambiar objetivo con izquierda/derecha
			if (Input.IsActionJustPressed("ui_left"))
			{
				GD.Print("Flecha visible moviendose a la izq");
				this.ChangeTarget(-1);
			}
			else if (Input.IsActionJustPressed("ui_right"))
			{
				GD.Print("Flecha visible moviendose a la der");
				this.ChangeTarget(1);
			}
		}
	}

	private void ChangeMenu(int c)
	{
		switch (c)
		{
			case 0:
				actingMenu.Visible = true;
				specialMenu.Visible = false;
				selecting.Visible = false;
				flecha.ShowArrow(false);
				this.selectingTarget = false;
				attack.GrabFocus();
				actor.passData().atqBasico.erraseTarget();
				actor.passData().mov1.erraseTarget();
				actor.passData().mov2.erraseTarget();
				actor.passData().mov3.erraseTarget();
				actor.passData().mov4.erraseTarget();
				foreach (Fighter f in enemieslist){
					f.DetenerParpadeo();
				}
				for (int i = 0; i < indexes.Length; i++){
					indexes[i] = false;  
				}
				break;
			case 1:
				specialMenu.Visible = true;
				actingMenu.Visible = false;
				selecting.Visible = false;
				flecha.ShowArrow(false);
				this.selectingTarget = false;
				mov1.GrabFocus();
				break;
			case 2:
				actingMenu.Visible = false;
				specialMenu.Visible = false;
				selecting.Visible = true;
				selecting.Modulate = new Color(1, 1, 1, 0); // Hacerlo invisible
				inv.GrabFocus();
				this.StartTargetSelection();
				break;
		}
	}

	private void OnAttackButtonDown()
	{
		GD.Print("Atacar");
		mov_actual = actor.passData().atqBasico;
		num_selec = mov_actual.num_objetivos;
		if(num_selec > enemieslist.Count)
			num_selec = enemieslist.Count;
		ChangeMenu(2);
		
	}

	private void OnSpecialButtonDown()
	{
		GD.Print("Especial");
		ChangeMenu(1);
	}

	private void OnBagButtonDown()
	{
		GD.Print("Bolsa");
	}

	private void OnGuardButtonDown()
	{
		GD.Print("Guardia");
	}
	
	private void OnMov1ButtonDown(){
		GD.Print("Especial1");
		mov_actual = actor.passData().mov1;
		num_selec = mov_actual.num_objetivos;
		if(num_selec > enemieslist.Count)
			num_selec = enemieslist.Count;
		ChangeMenu(2);
	}
	
	private void OnMov2ButtonDown(){
		GD.Print("Especial2");
		mov_actual = actor.passData().mov2;
		num_selec = mov_actual.num_objetivos;
		if(num_selec > enemieslist.Count)
			num_selec = enemieslist.Count;
		ChangeMenu(2);
	}
	
	private void OnMov3ButtonDown(){
		GD.Print("Especial3");
		mov_actual = actor.passData().mov3;
		num_selec = mov_actual.num_objetivos;
		if(num_selec > enemieslist.Count)
			num_selec = enemieslist.Count;
		ChangeMenu(2);
	}
	
	private void OnMov4ButtonDown(){
		GD.Print("Especial4");
		mov_actual = actor.passData().mov4;
		num_selec = mov_actual.num_objetivos;
		if(num_selec > enemieslist.Count)
			num_selec = enemieslist.Count;
		ChangeMenu(2);
	}
	
	private void OnInvButtonDown(){
		GD.Print("Target CONFIRM");
		this.ConfirmTarget();
	}
	
	public void receiveLists(List<Fighter> ene, List<Fighter> ally){
		this.allylist = ally;
		this.enemieslist = ene;
	}
	
	private void StartTargetSelection(){
		if (this.enemieslist.Count == 0){
			GD.PrintErr("No hay enemigos disponibles.");
			return;
		}
		
		//-----------------------
		
		
		//-----------------------
		
		this.selectingTarget = true;
		// Seleccionar el primer enemigo por defecto
		currentTargetIndex = 0;
		this.MoveArrowToCurrentTarget();
	}
	
	private void ChangeTarget(int direction)
	{
		if (this.enemieslist.Count == 0) return;
		// Mover en el array de enemigos (circular)
		this.enemieslist[currentTargetIndex].DetenerParpadeo();
		currentTargetIndex = (currentTargetIndex + direction + this.enemieslist.Count) % this.enemieslist.Count;
		while(indexes[currentTargetIndex])
			currentTargetIndex++;
		MoveArrowToCurrentTarget();
	}

	private void MoveArrowToCurrentTarget()
	{
		if (this.enemieslist.Count == 0) return;

		Fighter selectedEnemy = this.enemieslist[currentTargetIndex];
		selectedEnemy.Parpadear();
		// Mover la flecha sobre el enemigo
		flecha.MoveArrow(selectedEnemy.GetPosition() + new Vector2(0, 100)); // Ajustar la posición
		GD.Print("Flecha visible");
		flecha.ShowArrow(true);
	}

	private void ConfirmTarget()
	{
		mov_actual.addTarget(currentTargetIndex);
		num_selec--;
		if(num_selec == 0){
			foreach (int i in mov_actual.objetivos){
				this.enemieslist[i].DetenerParpadeo();
				GD.Print($"¡Enemigo {this.enemieslist[i].Name} seleccionado!");
			}
			mov_actual.erraseTarget();
			selectingTarget = false;
			flecha.ShowArrow(false);
			for (int i = 0; i < indexes.Length; i++){
				indexes[i] = false; 
			}
			ChangeMenu(0);
		}
		else{
			indexes[currentTargetIndex] = true;
			this.ChangeTarget(1);
		}
	}

}
