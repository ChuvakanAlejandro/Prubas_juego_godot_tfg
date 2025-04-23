using Godot;
using System;

public partial class ChuvakanMovimientoBasico : Movimiento{
	
	
	public ChuvakanMovimientoBasico(int l) {
		this.effectObj = Effect_Obj.Enemy;
		this.num_objetivos = 1;
		
		this.hurtful = true;
		this.status = false;
		assingLevel(l);
	}
	
	public override void efecto(){
		//Logica del movimiento;
		int random_number;
		potencia = 8 + this.casterLevel;
		//this.hurtTargets(potencia);
		/*if(this.casterLevel >= 8){
			Random rand = new Random();
			random_number = rand.Next(0, 1001);
			if(random_number == 0){
				this.origen.passData().estadoManager.AplicarEstado(Estado.Aturdido,2,0);
			}else if(random_number >= 250 && random_number <= 500){
				this.origen.passData().restoreMP((int) this.origen.passData().giveMAXMP()/2);
			}
		}
		*/
		GD.Print("Chuvakan va ha hacer su ataque basico!");
		//this.hurtTargets(potencia);
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < 8){
			return "Golpe Improvisado";
		}else{
			return "Enfocarse";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < 8){
			return "Un golpe basico que hace daño a un enemigo.";
		}else{
			return "Un golpe basico que hace daño a un enemigo. Puede tener un efecto secundario.";
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
		coste = 0;
		if(this.casterLevel >= 8){
			potencia = 9 + this.casterLevel;
		}else{
			potencia = 8 + this.casterLevel;
		}
		
	}
	
	
}
