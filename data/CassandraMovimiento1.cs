using Godot;
using System;

public partial class CassandraMovimiento1 : Movimiento{
	
	
	public CassandraMovimiento1(int l){
		this.effectObj = Effect_Obj.Enemy;
		this.num_objetivos = 2;
		hurtful = true;
		assingLevel(l);
	}
	
	public override void efecto(){
		//Logica del movimiento;
		this.origen.passData().removeMP(coste);
		GD.Print("Cassandra va ha hacer su ataque especial!");
		//this.hurtTargets(potencia);
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < 4){
			return "Brecha umbría";
		}else{
			return "Ruptura";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < 4){
			return "Ataque especial que ataca a dos enemigos haciendo daño moderado.";
		}else{
			return "Movimiento especial que ataca a todos enemigos haciendo daño moderado.";
		}
	}
	public override bool affectsAllTeam(){
		if(this.casterLevel < 4){
			return false;
		}else{
			return true;
		}
	}
	public override bool moveIsAvailable(){
		return true;
	}
	public override void assingLevel(int l){
		this.casterLevel = l;
		if(this.casterLevel >= 4){
			potencia = 8 + this.casterLevel;
			coste = 7;
		}else{
			potencia = 7 + this.casterLevel;
			coste = 4;
		}
	}
}
