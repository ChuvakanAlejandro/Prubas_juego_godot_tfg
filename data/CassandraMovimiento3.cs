using Godot;
using System;

public partial class CassandraMovimiento3 : Movimiento{
	
	
	public CassandraMovimiento3(){
		this.effectObj = Effect_Obj.Enemy;
		this.num_objetivos = 1;
	}
	
	public override void efecto(){
		//Logica del movimiento;
		GD.Print("Cassandra usa Sello arcano o Marca rúnica!");
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < 6){
			return "Sello arcano";
		}else{
			return "Marca rúnica";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < 6){
			return "Cassandra aplica un hechizo neutralizador a un objetivo haciendole un poco de daño y aplicandole el efecto de Sellado durante el siguiente turno del objetivo.";
		}else{
			return "Cassandra aplica un hechizo drenador a un objetivo haciendole daño y aplicandole el efecto de Sellado durante el siguiente turno del objetivo. Además se cura 50% del daño realizado.";
		}
	}
	public override bool affectsAllTeam(){
		return false;
	}
	public override bool moveIsAvailable(){
		if(this.casterLevel < 2){
			return false;
		}else{
			return true;
		}
	}
	public override int giveCost(){
		if(this.casterLevel < 4){
			return 5;
		}else{
			return 10;
		}
	}
	
}
