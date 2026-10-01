using Godot;
using System;

public partial class Slot : PanelContainer
{
    private Weapon weapon;
    [Export] private Personagem player;

    [Export] public Weapon Weapon
    {
        get => weapon;
        set
        {
            weapon = value;

            if (value != null)
            {
                GetNode<TextureRect>("TextureRect").Texture = value.Sprite;
                GetNode<Timer>("Cooldown").WaitTime = value.Cooldown;
            }
        }
    }

    private void _onCoolDownTimeOut()
    {
        if (weapon == null || player == null)
            return;
        GetNode<Timer>("Cooldown").WaitTime = weapon.Cooldown;
        weapon.Activate(player, player.getNearestEnemy(), GetTree());
    }
}
