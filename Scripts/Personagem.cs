using Godot;
using System;

public partial class Personagem : CharacterBody2D
{
    [Export] public float velocidade = 400.0f;

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

