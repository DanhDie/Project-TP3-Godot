using Godot;
using System;

public partial class CursorManager : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.SetCustomMouseCursor(GD.Load<Texture2D>("res://Assets/Cursor/target_a.png"));
		Input.SetCustomMouseCursor(GD.Load<Texture2D>("res://Assets/Cursor/hand_small_open.png"), Input.CursorShape.PointingHand);
	}
}
