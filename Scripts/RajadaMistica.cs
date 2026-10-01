using Godot;
using System;

public partial class RajadaMistica : Area2D
{
    [Export] float velocidade = 150f;
    [Export] float aceleracao = 1.1f;
    [Export] float velocidadeMaxima = 500;
    [Export] float alcance = 1000;
    [Export] float dano = 2;
    private float distanciaPercorrida = 0;
    [Export] private float taxaCrescimento = 1.01f;

    AudioStreamPlayer audio;
    public override void _Ready()
    {
        Scale = new Vector2(.5f, .5f);
        audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        TocarSom();
    }
    public override void _PhysicsProcess(double delta)
    {
        moverProjetil(Rotation, (float)delta);
        aumentarProjetil();

    }

    private void moverProjetil(float rotation, float delta) {
        Vector2 direcao = Vector2.Right.Rotated(rotation);
        Position += direcao * velocidade * delta;
        distanciaPercorrida += velocidade * delta;
        if (distanciaPercorrida > alcance) {
            this.QueueFree();
        }
        if (velocidade <= velocidadeMaxima)
        {
            velocidade *= aceleracao;
        }
    }

    private void _on_body_entered(CollisionObject2D body) {
        this.QueueFree();
        if (body.HasNode("HealthComponent"))
        {
            HealthComponent bodyHealth = body.GetNode<HealthComponent>("HealthComponent");
            bodyHealth.takeDamage(dano);
            LifeBar lifeBar = body.GetNode<LifeBar>("LifeBar");
            lifeBar.atualizarVida();
        }
    }

    private void aumentarProjetil()
    {
        if (Scale.X < 1.0f)
        {
            Scale *= taxaCrescimento;

            // Impede que passe de 1
            Scale = Scale.Clamp(
                new Vector2(0.0f, 0.0f),
                Vector2.One
            );
        }
    }
    public void TocarSom()
    {
        audio.PitchScale = (float)GD.RandRange(1.2, 1.5);
        audio.Play();
    }

}