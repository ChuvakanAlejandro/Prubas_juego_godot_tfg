using Godot;
using System;

public partial class ChuvakanMovimientoBasico : Movimiento{
	
	
	public ChuvakanMovimientoBasico(){
		this.effectObj = Effect_Obj.Enemy;
		this.num_objetivos = 1;
	}
	
	public override void efecto(){
		//Logica del movimiento;
		GD.Print("Chuvakan va ha hacer su ataque basico!");
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < 3){
			return "Golpe Improvisado";
		}else{
			return "Enfocarse";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < 9){
			return "Un golpe basico que hace daño a un enemigo.";
		}else{
			return "Un golpe basico que hace daño a un enemigo. Tiene un 50% de devolver un tercio de maná";
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
