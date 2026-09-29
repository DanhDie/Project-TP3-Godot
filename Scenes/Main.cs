using Godot;
using System;

public partial class Main : Node2D
{
    GameManager gameManager;
	public override void _Ready()
	{
        gameManager = GetNode<GameManager>("/root/GameManager");
        gameManager.timer = 0;
    }
}
