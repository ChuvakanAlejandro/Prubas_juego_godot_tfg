using Godot;
using System;

[GlobalClass]
public partial class Entity : Resource{
	[Export] public int Level {  get; set; } = 1;
	
	[Export] public int Health {  get; set; }= 1;
	[Export] public int Mana {  get; set; }= 1;
	
	[Export] public int ATQBuf {  get; set; }= 0;
	[Export] public int ATQDeBuf {  get; set; }= 0;
	
	[Export] public int DEFBuf {  get; set; }= 0;
	[Export] public int DEFDeBuf {  get; set; }= 0;
	
	[Export] public bool ControlPlayer {  get; set; } = true;
	[Export] public bool Turn {  get; set; } = true;
	
	[Export] public int[] TrueHealth;
	[Export] public int[] TrueAttack;
	[Export] public int[] TrueDefense;
	[Export] public int[] TrueMana;
	[Export] public int[] TrueSpeed;
	
	public virtual void levelUp(){
		Level++;
	}
	
	public void isMyTurn(){
		Turn = true;
	}
	public void passTurn(){
		Turn = false;
	}
	public int giveDMG(){
		return TrueAttack[Level-1];
	}
	public int giveDEF(){
		return TrueDefense[Level-1];
	}
	public int giveMAXHP(){
		return TrueAttack[Level-1];
	}
	public int giveMAXMP(){
		return TrueDefense[Level-1];
	}
	public int giveSP(){
		return TrueSpeed[Level-1];
	}
}
