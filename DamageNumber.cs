using Godot;
using System;

public partial class DamagePopup : Control
{
	[Export] public float Lifetime = 1.0f;
	private float _timer = 0f;

	public void SetDamage(int amount)
	{
		GetNode<Label>("Label").Text = amount.ToString();
	}

	public override void _Process(double delta)
	{
		_timer += (float)delta;
		Position += new Vector2(0, -30) * (float)delta; // se mueve hacia arriba
		if (_timer >= Lifetime)
			QueueFree();
	}
}
