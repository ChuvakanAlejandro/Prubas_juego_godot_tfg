using Godot;
using System;

public partial class CustomSignals : Node
{
	[Signal]
	public delegate void OnDialogPassEventHandler();
	[Signal]
	public delegate void OnDialogRequestedEventHandler(string text);
	[Signal]
	public delegate void OnBattleMoveEventHandler(Fighter actor, Movimiento mov_actual);
	[Signal]
	public delegate void OnBattleEffectDialogRequestedEventHandler();
	
	[Signal]
	public delegate void OnMoveResolvedEventHandler();
	
}
