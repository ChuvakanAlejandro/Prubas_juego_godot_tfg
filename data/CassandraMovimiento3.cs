using Godot;
using System;

public partial class CassandraMovimiento3 : Movimiento{
	
	
	public CassandraMovimiento3(int l) {
		this.effectObj = Effect_Obj.Enemy;
		this.num_objetivos = 1;
		this.evolucion = 6;
		this.hurtful = true;
		this.status = true;
		this.prime_status  = new Estado[] {Estado.Sellado};
		assingLevel(l);
	}
	
	public override void efecto(){
		//Logica del movimiento;
		this.origen.passData().removeMP(coste);
		//this.objetivos[0].passData().estadoManager.AplicarEstado(Estado.Sellado,1,0);
		//this.objetivos[0].ActualizarIconosEstado();
		//this.putEffectsOnTargets(100, prime_status, 2, 25);
		GD.Print("Cassandra va ha hacer su ataque especial!");
		//curacion = (int) (this.hurtTargets(potencia) / 2);
		if(this.casterLevel <= this.evolucion){
			//this.origen.passData().restoreHP(curacion);
		}
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < this.evolucion){
			return "Sello arcano";
		}else{
			return "Marca rúnica";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < this.evolucion){
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
	public override void assingLevel(int l){
		this.casterLevel = l;
		if(this.casterLevel >= this.evolucion){
			coste = 6;
			potencia = 5 + (int) (1.5 * this.casterLevel);
		}else{
			coste = 4;
			potencia = 5 + this.casterLevel;
		}
	}
	
}
