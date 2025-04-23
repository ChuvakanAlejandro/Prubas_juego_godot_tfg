using Godot;
using System;

public partial class CuadroTexto : Control
{
	private CustomSignals customSignals;
	
	private float characterDelay = 0.01f; // Tiempo entre letras
	private string fullText = "";
	private Label label;
	private Panel panel;
	private int currentCharIndex = 0;
	private float timeAccumulator = 0f;
	
	private bool isWriting = false;

	public override void _Ready()
	{
		//customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		//customSignals.OnDialogRequested += ShowDialog;
		panel = GetNode<Panel>("Panel");
		label = GetNode<Label>("Panel/MarginContainer/Label");
		label.Text = "";
		panel.Visible = false;
		//customSignals.OnDialogRequested += ShowDialog;
	}

	public override void _Process(double delta)
	{
		if (isWriting && currentCharIndex < fullText.Length)
		{
			timeAccumulator += (float)delta;
			if (timeAccumulator >= characterDelay)
			{
				timeAccumulator = 0f;
				currentCharIndex++;
				label.Text = fullText.Substring(0, currentCharIndex);
			}
		}
	}
	
	public void ShowDialog(string text)
	{
		fullText = text;
		currentCharIndex = 0;
		timeAccumulator = 0f;
		isWriting = true;
		label.Text = "";
		panel.Visible = true;
	}
}
