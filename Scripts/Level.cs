using Godot;
using System;

public partial class Level : Label
{
    [Export] private Personagem player; 
    public override void _Process(double delta)
    {
        atualizarLVL();
    }

    public void atualizarLVL()
    {
        Text = "Level "+ player.getLevel();
    }
}
