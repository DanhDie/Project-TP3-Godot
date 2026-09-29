using Godot;
using System;

public partial class Personagem : CharacterBody2D
{
    [Export] public float velocidade = 400.0f;
    [Export] private AnimatedSprite2D animatedSprite;
    [Export] private PackedScene tiroAtual;
    [Export] private float tempoInvul = 1.0f;

    private CollisionShape2D collisionShape;
    private HealthComponent healthPlayer;


    private enum Estado
    {
        IdleFrente,
        IdleCostas,
        IdleLado,
        WalkFrente,
        WalkCostas,
        WalkLado
    }

    private Estado estadoAtual = Estado.IdleFrente;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        healthPlayer = GetNode<HealthComponent>("HealthComponent");
        collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
    }
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("atirar"))
        {
            Atirar();
        }
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

        AtualizarEstado(direcao);
        AtualizarAnimacao();

        MoveAndSlide();
    }

    private void AtualizarEstado(Vector2 direcao)
    {
        if (direcao == Vector2.Zero)
        {
            // Mantém a direção anterior, mas troca Walk por Idle
            estadoAtual = estadoAtual switch
            {
                Estado.WalkFrente => Estado.IdleFrente,
                Estado.WalkCostas => Estado.IdleCostas,
                Estado.WalkLado => Estado.IdleLado,
                _ => estadoAtual
            };

            return;
        }

        if (Mathf.Abs(direcao.X) > Mathf.Abs(direcao.Y))
        {
            estadoAtual = Estado.WalkLado;
            animatedSprite.FlipH = direcao.X < 0;
        }
        else if (direcao.Y < 0)
        {
            estadoAtual = Estado.WalkCostas;
        }
        else
        {
            estadoAtual = Estado.WalkFrente;
        }
    }

    private void AtualizarAnimacao()
    {
        switch (estadoAtual)
        {
            case Estado.IdleFrente:
                animatedSprite.Play("idle_frente");
                break;

            case Estado.IdleCostas:
                animatedSprite.Play("idle_costas");
                break;

            case Estado.IdleLado:
                animatedSprite.Play("idle_lado");
                break;

            case Estado.WalkFrente:
                animatedSprite.Play("walk_frente");
                break;

            case Estado.WalkCostas:
                animatedSprite.Play("walk_costas");
                break;

            case Estado.WalkLado:
                animatedSprite.Play("walk_lado");
                break;
        }
    }

    private void Atirar()
    {
        Node2D bala = tiroAtual.Instantiate<Node2D>();
        GetTree().CurrentScene.AddChild(bala);

        bala.GlobalPosition = GlobalPosition;

        Vector2 direcao = GetGlobalMousePosition() - GlobalPosition;
        bala.Rotation = direcao.Angle();
    }

    private void OnSelfDamageBodyEntered(Node2D body)
    {
        if (body is Inimigo inimigo)
        {
            healthPlayer.takeDamage(inimigo.dano);
        }
    }
}