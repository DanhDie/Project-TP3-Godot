using Godot;
using System;

public partial class Inimigo : CharacterBody2D
{
	[Export] private float velocidade = 200.0f;
	[Export] public int dano = 2;
	[Export] public Personagem player;
	[Export] private AnimatedSprite2D animatedSprite;

	private Area2D area2D;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		area2D = GetNode<Area2D>("Area2D");
		area2D.BodyEntered += OnArea2DAreaEntered;
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

	public void OnArea2DAreaEntered(Node2D body)
	{
		if (body is Personagem player)
		{
			player.GetNode<HealthComponent>("HealthComponent").takeDamage(dano);
		}
	}
}
