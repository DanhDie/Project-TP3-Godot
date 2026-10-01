using Godot;
using System;

public partial class LifeBar : TextureProgressBar
{
    [Export] private CharacterBody2D player;
    private HealthComponent healthPlayer;

    public override void _Ready()
    {
        healthPlayer = player.GetNode<HealthComponent>("HealthComponent");
        this.Hide();
    }

    public void atualizarVida()
    {
        this.Show();
        Value = (healthPlayer.getHealth() / healthPlayer.maxHealth) * 100f;
    }

}
