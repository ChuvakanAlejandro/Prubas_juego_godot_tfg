using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CuadroTexto : Control
{
	private CustomSignals customSignals;
	
	private float characterDelay = 0.01f; // Tiempo entre letras
	private string fullText = "";
	private Label label;
	private Panel panel;
	private Button boton;
	private AudioStreamPlayer2D typeSound;
	private int currentCharIndex = 0;
	private float timeAccumulator = 0f;
	
	private bool finishedTyping = false;
	private bool isWriting = false;
	
	private Movimiento movimientoUsado;

	public override void _Ready()
	{
		customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		customSignals.OnDialogRequested += ShowDialog;
		customSignals.OnBattleMove += prepareDialog;  
		customSignals.OnBattleEffectDialogRequested += prepareDialogConsecuence;
		typeSound = GetNode<AudioStreamPlayer2D>("TypeSound");
		panel = GetNode<Panel>("Panel");
		label = GetNode<Label>("Panel/MarginContainer/Label");
		boton = GetNode<Button>("Panel/MarginContainer/Label/Button");
		label.Text = "";
		panel.Visible = false;
		boton.Visible = false;
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
				 if (!finishedTyping)
					typeSound.Play();
				if (currentCharIndex == fullText.Length){
					finishedTyping = true;
					boton.Visible = true;
					boton.GrabFocus();
				}
			}
		}
	}
	public void OnButtonPressed(){
		GD.Print("Boton dado.");
		panel.Visible = false;
		customSignals.EmitSignal(nameof(CustomSignals.OnDialogPass));
		
	}
	
	public void ShowDialog(string text)
	{
		fullText = text;
		currentCharIndex = 0;
		timeAccumulator = 0f;
		isWriting = true;
		label.Text = "";
		panel.Visible = true;
		boton.Visible = false;
		finishedTyping = false;
	}
	
	public void prepareDialog(Fighter actor, Movimiento mov_actual){
		movimientoUsado = mov_actual;
		string dialogo = actor.passData().Name  + " ha usado " + movimientoUsado.giveTitulo(); 
		if(movimientoUsado.whoAffects() != 3){
			dialogo  = $"{dialogo} en ";
			if(movimientoUsado.affectsAllTeam()){
				if(movimientoUsado.whoAffects() == 0)
					dialogo  = $"{dialogo}su equipo";
				else if(movimientoUsado.whoAffects() == 1)
					dialogo  = $"{dialogo}el equipo enemigo";
			}else{
				dialogo  = $"{dialogo}{movimientoUsado.objetivos[0].passData().Name}";
				if(movimientoUsado.objetivos.Count > 1 ){
					int i = 1;
					while(i <  movimientoUsado.objetivos.Count-1){
						dialogo = $"{dialogo}, {movimientoUsado.objetivos[i].passData().Name}";
						i++;
					}
					dialogo = $"{dialogo} y {movimientoUsado.objetivos[i].passData().Name}";
				}
			}
		}
		dialogo = $"{dialogo}.";
		GD.Print($"Dialogo preparado: {dialogo}");
		//dialog.ShowDialog(dialogo);
		customSignals.EmitSignal(nameof(CustomSignals.OnDialogRequested), dialogo);
	}
	
	public void prepareDialogConsecuence(){
		string dialogo = "";
		string affected = "";
		if(movimientoUsado.someAffected()){
			var entry = movimientoUsado.afectados.First();
			affected = entry.Key; 
			dialogo  = $"{affected} ha sido afectado por";
			List<Estado> estados = entry.Value;
			int i = 0;
			dialogo = $"{dialogo} {EstadosATexto(estados[i])}";
			i++;
			if(i <  estados.Count){
				while(i <  estados.Count-1){
					dialogo = $"{dialogo}, {EstadosATexto(estados[i])}";
					i++;
				}
				dialogo = $"{dialogo} y {EstadosATexto(estados[i])}";
			}
		}
		dialogo = $"{dialogo}.";
		GD.Print($"Dialogo preparado: {dialogo}");
		movimientoUsado.removeAffected(affected);
		//dialog.ShowDialog(dialogo);
		customSignals.EmitSignal(nameof(CustomSignals.OnDialogRequested), dialogo);
	}
	
	public string EstadosATexto(Estado e){
		switch(e){
			case Estado.BuffDMG:
				return "un potenciador de daño";
			case Estado.DeBuffDMG:
				return "una reduccion del de daño";
			case Estado.BuffDEF:
				return "un potenciador de defensa";
			case Estado.DeBuffDEF:
				return "una reduccion del de defensa";
			case Estado.BuffVEL:
				return "un potenciador de velocidad";
			case Estado.DeBuffVEL:
				return "una reduccion del de velocidad";
			case Estado.Aturdido:
				return "aturdimiento";
			case Estado.Sellado:
				return "sello magico";
			case Estado.Bloqueo:
				return "bloqueo de defensas";
			case Estado.Sangrado:
				return "sangrado";
			case Estado.Envenenado:
				return "veneno";
			case Estado.Regeneracion:
				return "regeneracion pasiva de vida";
			case Estado.Energetico:
				return "regeneracion pasiva de maná";
			case Estado.Evasion:
				return "evasión";
			case Estado.Marca_del_cazador:
				return "la marca del cazador";
			case Estado.Creacion:
				return "el potenciador de Alex";
			case Estado.Vanguardia:
				return "la proteccion de Vyls";
			default:
				return "un estado no reconocido (espera, que?)";
		}
	}
}
