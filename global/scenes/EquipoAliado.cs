using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class EquipoAliado : Node2D
{
	
	private Fighter Alex;
	private Fighter Cass;
	private List<Fighter> list;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Alex = GetNode<Fighter>("Personaje_Chuvakan");
		Cass = GetNode<Fighter>("Personaje_Cassandra");
		prepare_AllyList();
	}
	
	public void prepare_AllyList(){
		list = new List<Fighter>{
			Alex,
			Cass
		};
		foreach (Fighter f in list){
			Entity c = f.passData();
			GD.Print("NAME: " + c.Name);
			GD.Print("LEVEL: " + c.Level);
			GD.Print("HP: " + c.giveMAXHP());
			GD.Print("MP: " + c.giveMAXMP());
			GD.Print("DMG: " + c.giveDMG());
			GD.Print("DEF: " + c.giveDEF());
			GD.Print("SP: " + c.giveSP());
			GD.Print("---------------------------");
		}
	}
	
	public List<Fighter> giveList(){
		return list;
	}
}
