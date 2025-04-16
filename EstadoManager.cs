using Godot;
using System;
using System.Collections.Generic;  // This is the library that includes Dictionary


public enum Estado
{
	BuffDMG,
	DeBuffDMG,
	BuffDEF,
	DeBuffDEF,
	BuffVEL,
	DeBuffVEL,
	Aturdido,
	Sellado,
	Sangrado,
	Envenenado,
	Regeneracion,
	Creacion
	//etc
}

public partial class EstadoManager
{
	
	private Dictionary<Estado, int> estados = new Dictionary<Estado, int>();

	// Aplicar o renovar un estado
	public void AplicarEstado(Estado estado, int duracion){
		if (this.TieneEstado(estado))
			estados[estado] = duracion; //renovar duración
		else
			estados.Add(estado, duracion);//Insertar
	}

	// Reducir duración y eliminar expirados
	public void AvanzarTurno()
	{
		var expirados = new List<Estado>();

		List<Estado> claves = new List<Estado>(estados.Keys);
		
		foreach (var estado in claves){
			estados[estado]--;
			if (estados[estado] <= 0)
				expirados.Add(estado);
		}
		foreach (var estado in expirados)
			this.EliminarEstado(estado);
	}

	public bool TieneEstado(Estado estado){
		return estados.ContainsKey(estado);
	}
	
	public int GetDuracion(Estado estado){
		if (this.TieneEstado(estado)){
			return estados[estado];
		}else
			return 0;
	}

	public void EliminarEstado(Estado estado){
		estados.Remove(estado);
	}

	public Dictionary<Estado, int> GetEstados(){
		return estados;
	}
	
}
