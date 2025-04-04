using Godot;
using System;

public partial class Fighter : Node2D
{
	private Entity data;
	private AnimatedSprite2D animSprite;

	private void prepareData(int level){
		string name = this.Name.ToString();
		if(name.Contains("Chuvakan")){
			this.data = new ChuvakanData(level);
		}else if(name.Contains("Cassandra")){
			this.data = new CassandraData(level);
		}else if(name.Contains("Blue_Slime")){
			this.data = new SlimeInocenteData(level);
		}else if(name.Contains("Purple_Slime")){
			this.data = new SlimeVagoData(level);
		}else if(name.Contains("Orange_Slime")){
			this.data = new SlimeAgresivoData(level);
		}else if(name.Contains("Grey_Slime")){
			this.data = new SlimeTristeData(level);
		}else{
			GD.Print("No deberia entrar aqui.");
		}
	}
	
	public override void _Ready(){
		animSprite = GetNode<AnimatedSprite2D>("Sprites");
		this.prepareFighter(1);
	}
	
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
	
	public void prepareFighter(int level)
	{
		this.prepareData(level);
	}
	
	public Entity passData(){
		return this.data;
	}
}
