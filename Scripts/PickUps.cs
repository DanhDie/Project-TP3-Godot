using Godot;
using System;

public partial class PickUps : Area2D
{
	[Export] private String titulo;
	[Export] private Texture2D icone;
	[Export] private bool canFollow = false;
	protected Personagem player;

	private float speed = 170;
	private Vector2 direction;

    public override void _Ready()
    {
        Sprite2D sprite = GetNode<Sprite2D>("Sprite2D");
		sprite.Texture = icone;
    }
    public override void _PhysicsProcess(double delta)
    {
        if (player != null && canFollow)
        {
            direction = (player.Position - Position).Normalized();
            Position += direction * speed * (float)delta;
        }
    }
    public virtual void _OnPickUp(Node2D body)
	{
        destruirSe();

    }

    public void Follow(Personagem target)
    {
        player = target;
        canFollow = true;
    }
    public void destruirSe()
    {
        QueueFree();
    }
}
