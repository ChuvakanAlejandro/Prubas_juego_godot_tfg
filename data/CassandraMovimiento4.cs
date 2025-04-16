using Godot;
using System;

public partial class CassandraMovimiento4 : Movimiento{
	
	
	public CassandraMovimiento4(){
		this.effectObj = Effect_Obj.Enemy;
		this.num_objetivos = 1;
	}
	
	public override void efecto(){
		//Logica del movimiento;
		GD.Print("Cassandra usa Juicio o Exilio!");
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < 7){
			return "Juicio";
		}else{
			return "Exilio";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < 7){
			return "Cassandra transmite gran parte de su magia en este conjuro, haciendo gran daño al objetivo y pudiendo causar una reduccion de daño del 25% en su siguiente turno.";
		}else{
			return "Cassandra transmite gran parte de su magia en este conjuro, haciendo gran daño al objetivo y causandole una reduccion de daño del 25% en su siguiente turno.";
		}
	}
	public override bool affectsAllTeam(){
		return false;
	}
	public override bool moveIsAvailable(){
		if(this.casterLevel < 4){
			return false;
		}else{
			return true;
		}
	}
	public override int giveCost(){
		if(this.casterLevel < 4){
			return 10;
		}else{
			return 12;
		}
	}
	
}
