using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class Inimigo : CharacterBody2D, IDestrutivel
{
	[Export] private float velocidade = 200.0f;
	[Export] public int dano = 2;
	[Export] public int morteValue = 1;
    [Export] public Personagem player;
	[Export] private AnimatedSprite2D animatedSprite;
    [Export] private bool hasMultipleAnimations = false;
    
    
    private float separation;
    private Area2D area2D;

	GameManager gameManager;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		area2D = GetNode<Area2D>("Area2D");
		area2D.BodyEntered += OnArea2DAreaEntered;
		animatedSprite.Play("default");
        gameManager = GetNode<GameManager>("/root/GameManager");

    }
    public override void _Process(double delta)
    {
       // checkSeparation();
    }
    public override void _PhysicsProcess(double delta)
    {
        
        if (player != null)
        {
            Vector2 direcao = (player.Position - Position).Normalized();

            Velocity = direcao * velocidade;
            MoveAndSlide();

            if (hasMultipleAnimations)
            {
                AtualizarAnimacao(direcao);
            }
        }
    }

    private void AtualizarAnimacao(Vector2 direcao)
    {
        if (Mathf.Abs(direcao.X) > Mathf.Abs(direcao.Y))
        {
            if (direcao.X > 0)
            {
                animatedSprite.FlipH = false;
                animatedSprite.Play("walkLado");
            }

            else
            {
                animatedSprite.FlipH = true;
                animatedSprite.Play("walkLado");

            }
        }
        else
        {
            if (direcao.Y < 0)
                animatedSprite.Play("walkCostas");
            else
                animatedSprite.Play("default");
        }
    }

    public void OnArea2DAreaEntered(Node2D body)
	{
		if (body is Personagem player)
		{
			player.GetNode<HealthComponent>("HealthComponent").takeDamage(dano);
		}
	}

    public void destruirSe()
    {
		gameManager.aumentarPontuacao(morteValue);
        QueueFree();
    }

    private void checkSeparation()
    {
        separation = (player.Position - Position).Length();
        if(separation < player.getNearestEnemyDistance())
        {
            player.setNearestEnemy(this);
        }
    }


    public float getSeparation()
    {
        return separation;
    }
}
