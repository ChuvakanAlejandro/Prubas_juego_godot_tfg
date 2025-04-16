using Godot;
using System;

public partial class CassandraMovimiento1 : Movimiento{
	
	
	public CassandraMovimiento1(){
		this.effectObj = Effect_Obj.Enemy;
		this.num_objetivos = 2;
	}
	
	public override void efecto(){
		//Logica del movimiento;
		GD.Print("Cassandra usa Brecha Umbría o Ruptura!");
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
	public override int giveCost(){
		if(this.casterLevel < 4){
			return 4;
		}else{
			return 7;
		}
	}
	
}
