using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TurnManager{
	
	List<Fighter> allylist;
	List<Fighter> enemieslist;
	
	List<Fighter> turnOrder = new List<Fighter>();
	
	public TurnManager(List<Fighter> ally, List<Fighter> ene){
		allylist = ally;
		enemieslist = ene;
		prepareTurnOrder();
	}
	
	public void prepareTurnOrder(){
		// Combinar listas
		turnOrder = allylist.Concat(enemieslist).ToList();
		// Ordenar por velocidad total (TrueSpeed[level] + (buffs - debuffs)
		turnOrder = turnOrder.OrderByDescending(e => 
		{
			Entity data = e.passData();
			int baseSpeed = data.TrueSpeed[data.Level - 1]; 
			int buffedSpeed = baseSpeed + (baseSpeed * (data.VELBuf - data.VELDeBuf) / 100); 
			return buffedSpeed;
		}).ToList();
		// Debug: Imprimir el orden de turnos
		GD.Print("Orden de turnos:");
		foreach (Fighter f in turnOrder)
		{
			Entity entity = f.passData();
			GD.Print($"{entity.Name} - Velocidad: {entity.TrueSpeed[entity.Level-1]}");
		}
	}
}
