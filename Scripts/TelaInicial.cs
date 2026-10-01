using Godot;
using System;

public partial class TelaInicial : Control
{
    private CenterContainer options;
    GameManager gameManager;
    public override void _Ready()
	{
		GetNode<Button>("%Start").Pressed += _Play;
		GetNode<Button>("%Exit").Pressed += _QuitGame;
        gameManager = GetNode<GameManager>("/root/GameManager");
        options = GetNode<CenterContainer>("Options");

    }

    private void _QuitGame()
    {
		GetTree().Quit();
    }

    public void _Play()
	{
        TransicionScene transicao = GetNode<TransicionScene>("/root/TransicionScene");

        transicao.transicion("res://Scenes/main.tscn");
		gameManager.timer = 0;
	}
    public void _OnOptionsPressed()
    {
        options.Show();
    }
}
