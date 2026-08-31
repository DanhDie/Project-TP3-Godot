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
    public override void _PhysicsProcess(double delta)
    {
        moverProjetil(Rotation, (float)delta);
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
        }
    }
}