using Godot;
using System;

public partial class ChuvakanMovimiento1 : Movimiento{
	
	
	public ChuvakanMovimiento1(int l) {
		this.effectObj = Effect_Obj.Ally;
		this.num_objetivos = 1;
		
		this.hurtful = false;
		this.status = true;
		this.prime_status  = new Estado[] {Estado.BuffDMG,Estado.BuffDEF};
		assingLevel(l);
	}
	
	public override void efecto(){
		//Logica del movimiento;
		this.origen.passData().removeMP(coste);
		//this.putEffectsOnTargets(100, prime_status, 2, 25);
		//this.objetivos[0].passData().estadoManager.AplicarEstado(Estado.BuffDMG,1,20);
		//this.objetivos[0].passData().estadoManager.AplicarEstado(Estado.BuffDEF,1,20);
		//this.objetivos[0].ActualizarIconosEstado();
		//if(this.casterLevel >= 3){	
		//	this.origen.passData().estadoManager.AplicarEstado(Estado.BuffDMG,2,20);
		//	this.origen.passData().estadoManager.AplicarEstado(Estado.BuffDEF,2,20);
		//	this.origen.ActualizarIconosEstado();
		//}
		GD.Print("Chuvakan usa Motivación!");
	}
	public override void putEffectsOnTargets(double proba, Estado[] e, int dur, int ptg){
		Random rand = new Random();
		int num_max = 100, actual_prob, random_number;
		double aux = proba;
		while(aux < 1){
			aux *= 10;
			num_max *= 10;
		}
		actual_prob = (int) aux;
		for(int i = 0; i < objetivos.Count; i++){
			random_number = rand.Next(1, num_max+1);
			if(actual_prob > num_max){
				for(int j = 0; j < e.Length; j++){
					this.objetivos[i].passData().estadoManager.AplicarEstado(e[j],dur,ptg);
					if(!afectados.ContainsKey(objetivos[i].passData().Name))
						afectados[objetivos[i].passData().Name] = new List<Estado>();
					afectados[objetivos[i].passData().Name].Add(e[j]);
					if(this.casterLevel >= 3){	
						this.origen.passData().estadoManager.AplicarEstado(e[j],dur+1,ptg);
						this.origen.passData().estadoManager.AplicarEstado(e[j],dur+1,ptg);
					}
					afectados[origen.passData().Name].Add(e[j]);
				}
			}
		}
		
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < 3){
			return "Motivación";
		}else{
			return "Motivación inspiradora";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < 3){
			return "Alex motiva a uno de sus compañeros lo que aumenta el ataque y la defensa en un 20%.";
		}else{
			return "Alex motiva a uno de sus compañeros con confianza lo que aumenta el ataque y la defensa en un 20% tanto al aliado como a él";
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
		if(this.casterLevel >= 3){
			coste = 7;
		}else{
			coste = 5;
		}
	}
}
