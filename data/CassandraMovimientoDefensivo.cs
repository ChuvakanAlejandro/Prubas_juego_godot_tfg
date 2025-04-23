using Godot;
using System;

public partial class CassandraMovimientoDefensivo : Movimiento{
	
	
	public CassandraMovimientoDefensivo(int l) : base(){
		this.effectObj = Effect_Obj.Self;
		this.num_objetivos = 1;
		this.evolucion = 5;
		this.status = true;
		assingLevel(l);
	}
	
	public override void efecto(){
		//Logica del movimiento;
		int recuperacion_MP, def_ptg;
		if(this.casterLevel < this.evolucion){
			recuperacion_MP = (int) this.origen.passData().giveMAXMP()/3;
			def_ptg = 10;
		}else{
			recuperacion_MP = (int) this.origen.passData().giveMAXMP()/2;
			def_ptg = 20;
		}
		this.origen.passData().restoreMP(recuperacion_MP);
		//this.origen.passData().estadoManager.AplicarEstado(Estado.BuffDEF,2,def_ptg);
		//this.origen.ActualizarIconosEstado();
		//this.putEffectsOnTargets(100, prime_status, 2, 25);
		GD.Print("Cassandra va ha defenderse este turno!");
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < this.evolucion){
			return "Pantalla";
		}else{
			return "Reflejo";
		}
	}
	public override string giveDescripcion(){
		return "Escudo que aumenta la defensa hasta el proximo turno. Tambien devuelve parte del maná";
	}
	public override bool affectsAllTeam(){
		return false;
	}
	public override bool moveIsAvailable(){
		return true;
	}
	
}
