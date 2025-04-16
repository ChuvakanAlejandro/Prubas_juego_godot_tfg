using Godot;
using System;
using System.Collections.Generic;  // This is the library that includes Dictionary


public abstract partial class Movimiento{
	
	public enum Effect_Obj {Ally, Enemy, Self}
	
	public int dmg_origin { get; protected set; }
	public Effect_Obj effectObj { get; protected set; }
	
	public List<int> objetivos = new List<int>();
	public int origen;
	
	public int num_objetivos { get; protected set; } = 1; 
	
	public int casterLevel = 1;
	
	public abstract void efecto();
	
	public void assingTarget(int target){
		this.objetivos.Add(target);
	}
	public void assingCaster(int caster){
		this.origen = caster;
	}
	public void assingLevel(int l){
		this.casterLevel = l;
	}
	public void addTarget(int i){
		objetivos.Add(i);
	}
	public void erraseTarget(){
		while (objetivos.Count > 0){
			int last = objetivos[objetivos.Count - 1];
			Console.WriteLine("Popped: " + last);
			objetivos.RemoveAt(objetivos.Count - 1);
		}
	}
	
	public virtual string giveTitulo(){
		return "";
	}
	public virtual string giveDescripcion(){
		return "";
	}
	public virtual bool affectsAllTeam(){
		return false;
	}
	public virtual bool moveIsAvailable(){
		return false;
	}
	public virtual int giveCost(){
		return 0;
	}
	
}
