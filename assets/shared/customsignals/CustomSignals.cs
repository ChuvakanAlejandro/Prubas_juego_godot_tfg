using Godot;
using System;

public partial class CustomSignals : Node
{
	[Signal]
	public delegate void OnDialogConfirmedEventHandler();
	[Signal]
	public delegate void OnMoveResolvedEventHandler();
	[Signal]
	public delegate void OnDialogRequestedEventHandler(string text);
	
}
