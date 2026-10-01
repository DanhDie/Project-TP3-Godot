using Godot;
using System;

public partial class Personagem : CharacterBody2D, IDestrutivel
{
    [Signal] public delegate void PlayerMorreuEventHandler();
    [Export] public float velocidade = 400.0f;
    [Export] private AnimatedSprite2D animatedSprite;
    [Export] private PackedScene tiroAtual;
    [Export] private float tempoInvul = 1.0f;
    

    private CollisionShape2D collisionShape;
    private HealthComponent healthPlayer;
    private AudioStreamPlayer deathSound;

    private Inimigo nearestEnemy;
    private float nearestEnemyDistance = float.PositiveInfinity;


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
        deathSound = GetNode<AudioStreamPlayer>("DeathSound");
    }
    public override void _Process(double delta)
    {
        AtualizarNearestEnemy();
        if (Input.IsActionJustPressed("atirar"))
        {
            Atirar();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (nearestEnemy != null)
        {
            nearestEnemyDistance = nearestEnemy.getSeparation();
        }
        else
        {
            nearestEnemyDistance = float.PositiveInfinity;
        }
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

    public void destruirSe()
    {
        EmitSignal(SignalName.PlayerMorreu);
        deathSound.Play();
        GetTree().Paused = true;
    }

    public float getNearestEnemyDistance()
    {
        return nearestEnemyDistance;
    }
    public void setNearestEnemy(Inimigo enemy)
    {
        nearestEnemy = enemy;
        nearestEnemyDistance = enemy.getSeparation();
    }
    public Inimigo getNearestEnemy()
    {
        return nearestEnemy;
    }
    private void AtualizarNearestEnemy()
    {
        if (GodotObject.IsInstanceValid(nearestEnemy))
            return;

        nearestEnemy = null;
        nearestEnemyDistance = float.PositiveInfinity;

        foreach (Node node in GetTree().GetNodesInGroup("inimigos"))
        {
            if (node is Inimigo inimigo && GodotObject.IsInstanceValid(inimigo))
            {
                float distancia = GlobalPosition.DistanceTo(inimigo.GlobalPosition);

                if (distancia < nearestEnemyDistance)
                {
                    nearestEnemy = inimigo;
                    nearestEnemyDistance = distancia;
                }
            }
        }
    }
}