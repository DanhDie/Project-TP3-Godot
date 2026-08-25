using Godot;
using System;

public partial class Personagem : CharacterBody2D
{
	[Export] public float velocidade = 400.0f;
	
	[Export] private AnimationPlayer animationPlayer;
	
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direcao = Input.GetVector(
			"mover_esquerda",
			"mover_direita",
			"mover_cima",
            "mover_baixo"
		);

		Velocity = direcao * velocidade;

		MoveAndSlide();
	}
}
