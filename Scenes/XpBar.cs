using Godot;
using System;

public partial class XpBar : TextureProgressBar
{
    [Export] private Personagem player;

    public override void _Ready()
    {
        MaxValue = 5;
        Value = player.getXP();
    }
    public override void _Process(double delta)
    {
        atualizarXP();
    }

    public void atualizarXP()
    {
        Value = player.getXP();
        MaxValue = player.getXPtoNextLevel();
    }
}
