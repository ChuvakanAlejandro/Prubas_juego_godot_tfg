using Godot;
using System;

[GlobalClass]
public partial class CassandraData : Entity
{
	
	public CassandraData(){
		Level = 1;
		
		Health = 18;
		Mana = 18;
		
		ControlPlayer = true;
		Turn = false;
		
		TrueHealth = new int[] {18,20,22,24,26,28,30,32,34,36};
		TrueAttack = new int[] {7,9,11,13,15,17,19,21,23,25};
		TrueDefense = new int[] {4,5,6,7,8,9,10,11,12,13};
		TrueMana = new int[] {18,21,24,27,30,33,36,39,42,45};
		TrueSpeed = new int[] {6,7,8,9,10,11,12,13,14,15};
	}
	
	public override void levelUp(){
		Level++;
		Health += 2;
		Mana += 3;
	}
}
