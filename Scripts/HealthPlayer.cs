using Godot;
using System;

public partial class HealthPlayer : TextureProgressBar
{
	[Export] private CharacterBody2D player;
    private HealthComponent healthPlayer;

    public override void _Ready()
    {
        healthPlayer = player.GetNode<HealthComponent>("HealthComponent");
    }

    public override void _Process(double delta)
    {
        atualizarVida();
    }

    private void atualizarVida()
    {
        Value = healthPlayer.getHealth()*100 / healthPlayer.maxHealth;
    }
	
}
