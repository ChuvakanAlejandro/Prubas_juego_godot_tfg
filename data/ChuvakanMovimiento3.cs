using Godot;
using System;

public partial class ChuvakanMovimiento3 : Movimiento{
	
	
	public ChuvakanMovimiento3(int l){
		this.effectObj = Effect_Obj.Enemy;
		this.num_objetivos = 1;
		this.evolucion = 38;
		this.hurtful = true;
		this.prime_status  = new Estado[] {Estado.Aturdido};
		assingLevel(l);
	}
	
	public override void efecto(){
		int probabilidad = 30, random_number;
		//Random rand = new Random();
		//random_number = rand.Next(1, 101);
		this.origen.passData().removeMP(coste);
		
		GD.Print("Alex va ha hacer su ataque especial!");
		//this.hurtTargets(potencia);
		//this.putEffectsOnTargets(probabilidad, prime_status, 1, 0);
		//if(random_number >= 100-probabilidad){
			//this.objetivos[0].passData().estadoManager.AplicarEstado(Estado.Aturdido,1,0);
			//this.objetivos[0].ActualizarIconosEstado();
		//}
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < this.evolucion){
			return "Mazo personal";
		}else{
			return "Mola Mazo";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < this.evolucion){
			return "Alex dibuja su arma favorita y la hace realidad para hacer gran daño a un enemigo.";
		}else{
			return "Alex dibuja su arma favorita y la hace realidad para hacer gran daño a un enemigo. Este ataque puede aturdir al objetivo";
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
			coste = 9;
			potencia = 12 + this.casterLevel;
			this.status = true;
		}else{
			potencia = 11 + this.casterLevel;
			coste = 9;
		}
	}
	
}
