using Godot;
using System;

public partial class CassandraMovimiento4 : Movimiento{
	
	
	public CassandraMovimiento4(int l){
		this.effectObj = Effect_Obj.Enemy;
		this.num_objetivos = 1;
		this.evolucion = 7;
		this.hurtful = true;
		this.prime_status  = new Estado[] {Estado.DeBuffDMG};
		assingLevel(l);
	}
	
	public override void efecto(){
		//Logica del movimiento;
		int probabilidad = 30, random_number;
		Random rand = new Random();
		random_number = rand.Next(1, 101);
		this.origen.passData().removeMP(coste);
		
		GD.Print("Cassandra va ha hacer su ataque especial!");
		//this.hurtTargets(potencia);
		if(random_number >= 100-probabilidad){
			//this.objetivos[0].passData().estadoManager.AplicarEstado(Estado.DeBuffDMG,1,25);
			//this.objetivos[0].ActualizarIconosEstado();
		}
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < this.evolucion){
			return "Juicio";
		}else{
			return "Exilio";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < this.evolucion){
			return "Cassandra transmite gran parte de su magia en este conjuro, haciendo gran daño al objetivo y pudiendo causar una reduccion de daño del 25% en su siguiente turno.";
		}else{
			return "Cassandra transmite gran parte de su magia en este conjuro, haciendo gran daño al objetivo y causandole una reduccion de daño del 25% en su siguiente turno.";
		}
	}
	public override bool affectsAllTeam(){
		return false;
	}
	public override bool moveIsAvailable(){
		if(this.casterLevel < 3){
			return false;
		}else{
			return true;
		}
	}
	public override void assingLevel(int l){
		this.casterLevel = l;
		if(this.casterLevel >= this.evolucion){
			coste = 12;
			potencia = 10 + (int) (1.5 * this.casterLevel);
			this.status = true;
		}else{
			coste = 10;
			potencia = 10 + this.casterLevel;
		}
	}
}
