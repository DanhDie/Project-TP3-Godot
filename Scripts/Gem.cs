using Godot;
using System;

public partial class Gem : PickUps
{
	[Export] float xp;

	public override void _OnPickUp(Node2D body)
	{
		player.addXP(xp);
		destruirSe();
	}
}
