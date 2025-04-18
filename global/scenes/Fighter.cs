using Godot;
using System;

public partial class Fighter : Node2D
{
	private Entity data;
	private AnimatedSprite2D animSprite;
	private Tween parpadeoTween;

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
	
	public void Parpadear(){
		// Si ya hay un tween activo, lo eliminamos
		if (parpadeoTween != null && parpadeoTween.IsValid()){
			parpadeoTween.Kill();
			parpadeoTween = null;
		}
		parpadeoTween = GetTree().CreateTween();
		parpadeoTween.SetLoops(-1);
		// Oscurecer
		parpadeoTween.TweenProperty(animSprite, "modulate", new Color(0.4f, 0.4f, 0.4f), 0.15f)
					 .SetTrans(Tween.TransitionType.Sine)
					 .SetEase(Tween.EaseType.InOut);
		// Volver a normal
		parpadeoTween.TweenProperty(animSprite, "modulate", new Color(1f, 1f, 1f), 0.15f)
					 .SetTrans(Tween.TransitionType.Sine)
					 .SetEase(Tween.EaseType.InOut);
	}
	
	public void DetenerParpadeo(){
		if (parpadeoTween != null && parpadeoTween.IsValid())
		{
			parpadeoTween.Kill(); // Detiene el tween
			parpadeoTween = null;

			// (opcional) Restaurar color normal
			animSprite.Modulate = new Color(1f, 1f, 1f);
		}
	}
}
