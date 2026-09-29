using Godot;
using System;

public partial class Contador : Label
{
	GameManager gameManager;
	int segundos;
	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");
	}
	public override void _Process(double delta)
	{
		TimeSpan tempo = TimeSpan.FromSeconds(gameManager.timer);
		Text = tempo.ToString(@"mm\:ss");
	}
}
