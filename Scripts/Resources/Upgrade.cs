using Godot;
using System;

public partial class Upgrade : Resource
{
    [Export] public float damage;
    [Export] public float coolDown;
    [Export(PropertyHint.MultilineText)] public String description;



}
