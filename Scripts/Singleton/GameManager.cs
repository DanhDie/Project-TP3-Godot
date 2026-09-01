using Godot;
using System;

public partial class GameManager : Node
{
	public float timer = 0f;

	public override void _Process(double delta)
	{ 
		aumentarTempo(delta);
	}

	private void aumentarTempo(double delta)
	{
		timer += (float)delta;
	}

}
