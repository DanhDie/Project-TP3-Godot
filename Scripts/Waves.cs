using Godot;
using System;

public partial class Waves : Label
{
    GameManager gameManager;
    int segundos;
    public override void _Ready()
    {
        gameManager = GetNode<GameManager>("/root/GameManager");
    }
    public override void _Process(double delta)
    {
        int wave = (int)(gameManager.timer / 20) + 1;

        Text = "Onda " + wave;
    }
}
