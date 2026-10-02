using Godot;
using System;

public partial class Magnet : Area2D
{
	public void _OnAreaEnter(Area2D area)
	{
        if (area.HasMethod("Follow"))
        {
            area.Call("Follow", Owner);
        }
    }
}
