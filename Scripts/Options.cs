using Godot;
using System;

public partial class Options : CenterContainer
{
    public override void _Ready()
    {
        this.Hide();
    }
    public void _OnResumePressed()
    {
        this.Hide();
    }
}
