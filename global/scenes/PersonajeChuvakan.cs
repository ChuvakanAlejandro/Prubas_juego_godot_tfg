using Godot;
using System;

public partial class PersonajeChuvakan : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	private Entity data;
	private AnimatedSprite2D animSprite;
	
	public override void _Ready()
	{
		data = new ChuvakanData();
		animSprite = GetNode<AnimatedSprite2D>("Sprites_Chu");
		//GD.Print("Chuvakan_Health: ",data.Health);
		//GD.Print("Chuvakan_MaxHealth: ",data.TrueHealth[data.Level-1]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(data.Health > (int) data.TrueHealth[data.Level-1]/4){
			animSprite.Play("idle");
		}else{
			if(data.Health == 0){
				animSprite.Play("fainted");
			}else{
				animSprite.Play("idle_low");
			}
		}
	}
	
	public Entity passData(){
		return data;
	}
}
