using Godot;
using System;

public partial class IshimondoMovimientoDefensivo : Movimiento{
	
	
	public IshimondoMovimientoDefensivo() : base(){
		this.effectObj = Effect_Obj.Self;
		this.num_objetivos = 1;
	}
	
	public override void efecto(){
		//Logica del movimiento;
		int recuperacion_MP, def_ptg;
		def_ptg = 10;
		recuperacion_MP = (int) this.origen.passData().giveMAXMP()/3;
		this.origen.passData().restoreMP(recuperacion_MP);
		this.origen.passData().estadoManager.AplicarEstado(Estado.BuffDEF,2,def_ptg);
		if(this.casterLevel >= 4)
			this.origen.passData().estadoManager.AplicarEstado(Estado.Evasion,2,0);
		this.origen.ActualizarIconosEstado();
		GD.Print("Ishimondo va ha defenderse este turno!");
	}
	
	public override string giveTitulo(){
		if(this.casterLevel < 4){
			return "Hacerse bolita";
		}else{
			return "Reflejps felinos";
		}
	}
	public override string giveDescripcion(){
		if(this.casterLevel < 4){
			return "Maniobra que aumenta la defensa hasta el proximo turno. Tambien devuelve parte del maná.";
		}else{
			return "Maniobra que aumenta la defensa hasta el proximo turno. Tambien devuelve parte del maná y le da el estado evasivo.";
		}
	}
	public override bool affectsAllTeam(){
		return false;
	}
	public override bool moveIsAvailable(){
		return true;
	}
	public override bool enoughMana(){
		return true;
	}
	
}
