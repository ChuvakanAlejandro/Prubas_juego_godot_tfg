using Godot;
using System;

public partial class CassandraMovimientoBasico : Movimiento{
	
	
	public CassandraMovimientoBasico(){
		this.effectObj = Effect_Obj.Enemy;
		this.num_objetivos = 1;
	}
	
	public override void efecto(){
		//Logica del movimiento;
		GD.Print("Cassandra va ha hacer su ataque basico!");
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < 3){
			return "Pulso magico";
		}else{
			return "Impulso arcano";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < 9){
			return "Un disparo magico que hace daño a un enemigo.";
		}else{
			return "Un disparo magico que hace daño a un enemigo y restaura un podo de maná";
		}
	}
	public override bool affectsAllTeam(){
		return false;
	}
	public override bool moveIsAvailable(){
		return true;
	}
	public override int giveCost(){
		return 0;
	}
	
}
