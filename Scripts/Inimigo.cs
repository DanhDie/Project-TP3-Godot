using Godot;
using System;

public partial class Inimigo : CharacterBody2D
{
	[Export] private Personagem player;
	[Export] private float velocidade = 200.0f;
    public override void _Process(double delta)
	{
		//checar se player esta presente
		if (player != null)
		{
			Vector2 direcao = (player.Position - Position).Normalized();
			Velocity = direcao * velocidade;
			MoveAndSlide();

        }
	}
}
