using Godot;

[GlobalClass]
public partial class Projetil : Area2D
{
    [Export] public float Speed = 300f;
    [Export] public float Alcance = 1000f;
    [Export] public float Damage = 2f;

    private float distanciaPercorrida = 0;
    public Vector2 Direction { get; set; }

    private AudioStreamPlayer audio;

    public override void _Ready()
    {
        Scale = new Vector2(0.5f, 0.5f);

        audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        TocarSom();
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Direction * Speed * (float)delta;

        distanciaPercorrida += Speed * (float)delta;

        if (distanciaPercorrida > Alcance)
            QueueFree();
    }

    private void _on_body_entered(CollisionObject2D body)
    {
        QueueFree();

        if (body.HasNode("HealthComponent"))
        {
            HealthComponent bodyHealth =
                body.GetNode<HealthComponent>("HealthComponent");

            bodyHealth.takeDamage(Damage);

            LifeBar lifeBar =
                body.GetNode<LifeBar>("LifeBar");

            lifeBar.atualizarVida();
        }
    }

    public void TocarSom()
    {
        audio.PitchScale = (float)GD.RandRange(1.2, 1.5);
        audio.Play();
    }
}