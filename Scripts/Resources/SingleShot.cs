using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class SingleShot : Weapon
{
    public override void Activate(Node2D source, Node2D target, SceneTree sceneTree)
    {
        Shoot(source, target, sceneTree);
    }

    private void Shoot(Node2D source, Node2D target, SceneTree sceneTree)
    {
        if (target == null || !GodotObject.IsInstanceValid(target))
            return;

        var projectile = Projetil.Instantiate<Projetil>();

        projectile.Position = source.Position;

        Vector2 direcao = (target.Position - source.Position).Normalized();

        projectile.Damage = Damage;
        projectile.Speed = Speed;
        projectile.Direction = direcao;
        projectile.Rotation = direcao.Angle();

        sceneTree.CurrentScene.AddChild(projectile);
    }
}
