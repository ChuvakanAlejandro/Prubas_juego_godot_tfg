using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class EquipoAliado : Node2D
{
	
	private PersonajeChuvakan Alex;
	private PersonajeCassandra Cass;
	List<Entity> list;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Alex = (PersonajeChuvakan) GetNode<CharacterBody2D>("Personaje_Chuvakan");
		Cass = (PersonajeCassandra) GetNode<CharacterBody2D>("Personaje_Cassandra");
		prepare_AllyList();
	}
	
	public void prepare_AllyList(){
		List<Entity> list = new List<Entity>{
			Alex.passData(),
			Cass.passData()
		};
		//foreach (Entity c in list)
		//{
		//	GD.Print("Character_Speed: ",c.giveSP());
		//}
	}
	
	public List<Entity> giveList(){
		return list;
	}
}
