using Godot;
using System;

public partial class Slot : PanelContainer
{
    private Weapon weapon;

    [Export] private Personagem player;
    [Export]
    public Weapon Weapon
    {
        get => weapon;
        set
        {
            weapon = value;

            if (weapon != null)
            {
                weapon.Cooldown = 0.6f;
                weapon.level = 0;

                GetNode<TextureRect>("TextureRect").Texture = weapon.Sprite;
                GetNode<Timer>("Cooldown").WaitTime = weapon.Cooldown;
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

    public void upgradeWeapon()
    {
        weapon.level++;
        if (weapon.Cooldown > 0.15f)
        {
            weapon.Cooldown -= 0.05f;
        }
    }
}