using Godot;
using System;

[GlobalClass]
public partial class SlimeInocenteData : EnemyEntity
{
	public SlimeInocenteData(int level){
		Level = level;
		
		Health = TrueHealth[level];
		Mana = 0;
		
		ControlPlayer = true;
		Turn = false;
		
		beh_type = Aleatorio;
		
		TrueHealth = new int[] {12,15,18,21,24,27,30,33,36,39};
		TrueAttack = new int[] {3,4,5,6,7,8,9,10,11,12};
		TrueDefense = new int[] {4,5,6,7,8,9,10,11,12,13};
		TrueMana = new int[] {0,0,0,0,0,0,0,0,0,0};
		TrueSpeed = new int[] {5,6,7,8,9,10,11,12,13,14};
	}
}
