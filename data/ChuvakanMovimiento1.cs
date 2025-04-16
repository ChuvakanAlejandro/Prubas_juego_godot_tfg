using Godot;
using System;

public partial class ChuvakanMovimiento1 : Movimiento{
	
	
	public ChuvakanMovimiento1(){
		this.effectObj = Effect_Obj.Ally;
		this.num_objetivos = 1;
	}
	
	public override void efecto(){
		//Logica del movimiento;
		GD.Print("Chuvakan usa Motivación o Motivación inspirativa!");
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < 3){
			return "Motivación";
		}else{
			return "Motivación inspirativa";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < 3){
			return "Movimiento especial que aumenta el ataque y la defensa en un 20% de si mismo o de un compañero activo del grupo en su próximo turno.";
		}else{
			return "Movimiento especial que aumenta el ataque y la defensa en un 20% de si mismo y de un compañero activo del grupo en sus próximos 2 turnos.";
		}
	}
	public override bool affectsAllTeam(){
		return false;
	}
	public override bool moveIsAvailable(){
		return true;
	}
	public override int giveCost(){
		if(this.casterLevel < 4){
			return 5;
		}else{
			return 7;
		}
	}
	
}
