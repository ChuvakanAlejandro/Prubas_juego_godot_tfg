using Godot;
using System;

public partial class CassandraMovimiento2 : Movimiento{
	
	
	public CassandraMovimiento2(){
		this.effectObj = Effect_Obj.Self;
		this.num_objetivos = 1;
	}
	
	public override void efecto(){
		//Logica del movimiento;
		GD.Print("Cassandra usa Mente tranquila o Paz interior!");
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < 4){
			return "Mente tranquila";
		}else{
			return "Paz interior";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < 4){
			return "Cassandra relaja su mente y se centra en su objetivo lo que aumenta el ataque y la defensa del personaje en un 25% durante 2 turnos.";
		}else{
			return "Cassandra solo ve su objetivo lo que aumenta el ataque y la defensa del personaje en un 25% durante 2 turnos y obtiene el estado Reflectante.";
		}
	}
	public override bool affectsAllTeam(){
		return false;
	}
	public override bool moveIsAvailable(){
		return true;
	}
	public override int giveCost(){
		if(this.casterLevel < 4){
			return 4;
		}else{
			return 6;
		}
	}
	
}
