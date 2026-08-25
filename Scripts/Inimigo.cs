using Godot;
using System;

public partial class Inimigo : CharacterBody2D
{
	[Export] private Personagem player;
	[Export] private float velocidade = 200.0f;
    [Export] private AnimatedSprite2D animatedSprite;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite.Play("default");
    }
    public override void _PhysicsProcess(double delta)
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
