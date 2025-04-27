using Godot;
using System;

[GlobalClass]
public partial class ChuvakanData : Entity
{
	
	public ChuvakanData(int level){
		Level = level;
		
		Name = "Alex";
		
		ControlPlayer = true;
		Turn = false;
		
		TrueHealth = new int[] {20,23,26,29,32,35,38,41,44,47};
		TrueAttack = new int[] {5,6,7,8,9,10,11,12,13,14};
		TrueDefense = new int[] {5,6,7,8,9,10,11,12,13,14};
		TrueMana = new int[] {15,17,19,21,23,25,27,29,31,33};
		TrueSpeed = new int[] {5,7,9,11,13,15,17,19,21,23};
		
		Health = TrueHealth[level-1];
		Mana = TrueMana[level-1];
		
		this.estadoManager = new EstadoManager(this);
		
		mov1 = new ChuvakanMovimiento1(Level);
		mov2 = new ChuvakanMovimiento2(Level);
		mov3 = new ChuvakanMovimiento3(Level);
		mov4 = new ChuvakanMovimiento4(Level);
		atqBasico = new ChuvakanMovimientoBasico(Level);
		defBasico = new ChuvakanMovimientoDefensivo(Level);
	}
	
	public override void levelUp(){
		Level++;
		Health += 3;
		Mana += 2;
		this.atqBasico.assingLevel(this.Level);
		this.mov1.assingLevel(this.Level);
		this.mov2.assingLevel(this.Level);
		this.mov3.assingLevel(this.Level);
		this.mov4.assingLevel(this.Level);
		this.defBasico.assingLevel(this.Level);
	}
}
