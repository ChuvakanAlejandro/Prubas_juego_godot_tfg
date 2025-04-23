using Godot;
using System;
using System.Linq;
using System.Collections.Generic;  // This is the library that includes Dictionary


public abstract partial class Movimiento{
	
	public enum Effect_Obj {Ally, Enemy, Both, Self}
	public Effect_Obj effectObj { get; protected set; }
	public List<Fighter> objetivos = new List<Fighter>();
	public Fighter origen;
	public int num_objetivos { get; protected set; } = 1; 
	public int casterLevel = 1;
	
	public int coste = 0;
	public int potencia = 0;
	public int evolucion = 0;
	
	public bool hurtful = false;
	public bool status = false;
	
	protected Estado[] prime_status;
	
	private Dictionary<string, List<Estado>> afectados = new Dictionary<string,  List<Estado>>();
	
	
	public abstract void efecto();
	
	public void assingCaster(Fighter caster){
		this.origen = caster;
	}
	public bool enoughMana(){
		return this.origen.passData().giveMP() >= coste;
	}
	public void addTarget(Fighter i){
		objetivos.Add(i);
	}
	public void erraseTarget(){
		while (objetivos.Count > 0){
			objetivos.RemoveAt(objetivos.Count - 1);
		}
		afectados.Clear();
	}
	public int whoAffects(){
		switch(effectObj){
			case Effect_Obj.Ally:
				return 0;
			case Effect_Obj.Enemy:
				return 1;
			case Effect_Obj.Both:
				return 2;
			case Effect_Obj.Self:
				return 3;
		}
		return -1;
	}
	public bool someAffected(){
		return afectados.Count == 0;
	}
	
	public virtual int hurtTargets(int p){
		int formula = 0, dañoBufado,  defensaBufado, ATQOrigen, DEFOrigen, f1, f2;
		float porcentajeATQ, porcentajeDEF;
		
		porcentajeATQ = 1  + (origen.passData().giveDMGBuf() - origen.passData().giveDMGDeBuf()) / 100;
		ATQOrigen = origen.passData().giveDMG();
		dañoBufado = (int) (ATQOrigen * porcentajeATQ);
		f1 = ATQOrigen + dañoBufado;
		
		for(int i = 0; i < objetivos.Count; i++){
			porcentajeDEF  = 1  + (objetivos[i].passData().giveDEFBuf() - objetivos[i].passData().giveDEFDeBuf()) / 100;
			DEFOrigen = objetivos[i].passData().giveDEF();
			defensaBufado = (int) (DEFOrigen * porcentajeDEF);
			f2 = DEFOrigen + defensaBufado;
			formula = Math.Max(1,p+f1-f2);
			objetivos[i].passData().removeHP(formula);
		}
		return formula;
	}
	public virtual void putEffectsOnTargets(double proba, Estado[] e, int dur, int ptg){
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
			if(actual_prob + random_number > num_max){
				for(int j = 0; j < e.Length; j++){
					this.objetivos[i].passData().estadoManager.AplicarEstado(e[j],dur,ptg);
					if(!afectados.ContainsKey(objetivos[i].passData().Name))
						afectados[objetivos[i].passData().Name] = new List<Estado>();
					afectados[objetivos[i].passData().Name].Add(e[j]);
				}
			}
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
	public virtual void assingLevel(int l){
		this.casterLevel = l;
	}
	
}
