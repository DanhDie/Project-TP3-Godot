using Godot;
using System;

public partial class TelaInicial : Control
{
    GameManager gameManager;
    public override void _Ready()
	{
		GetNode<Button>("%Start").Pressed += _Play;
		GetNode<Button>("%Exit").Pressed += _QuitGame;
        gameManager = GetNode<GameManager>("/root/GameManager");
    }

    private void _QuitGame()
    {
		GetTree().Quit();
    }

    public void _Play()
	{
		GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
		gameManager.timer = 0;
	}
}
