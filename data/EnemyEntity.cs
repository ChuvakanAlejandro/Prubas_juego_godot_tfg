using Godot;
using System;

[GlobalClass]
public partial class EnemyEntity : Entity
{
	public enum Behaviour {Aleatorio, Agresivo, Tactico, Apoyo}
	
	[Export] public Behaviour beh_type { get; protected set; }
	
	
}
