using Godot;

public partial class Weapon : Resource
{
    [Export] public string Nome { get; set; }
    [Export] public Texture2D Sprite { get; set; }
    [Export] public float Damage { get; set; }
    [Export] public float Cooldown { get; set; }
    [Export] public float Speed { get; set; }
    [Export] public PackedScene Projetil { get; set; }
    public virtual void Activate(Node2D source, Node2D target, SceneTree sceneTree)
    {
    }
}