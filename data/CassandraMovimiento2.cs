using Godot;
using System;

public partial class CassandraMovimiento2 : Movimiento{
	
	
	public CassandraMovimiento2(int l){
		this.effectObj = Effect_Obj.Self;
		this.num_objetivos = 1;
		this.status = true;
		this.prime_status  = new Estado[] {Estado.BuffDMG,Estado.BuffDEF};
		assingLevel(l);
	}
	
	public override void efecto(){
		//Logica del movimiento;
		this.origen.passData().removeMP(coste);
		//this.putEffectsOnTargets(100, prime_status, 2, 25);
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
	public override void assingLevel(int l){
		this.casterLevel = l;
		if(this.casterLevel >= 4){
			coste = 6;
		}else{
			coste = 4;
		}
	}
	
}
