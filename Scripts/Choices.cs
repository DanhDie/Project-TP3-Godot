using Godot;
using System;

public partial class Choices : VBoxContainer
{
	[Export] HBoxContainer weapons;
    private PackedScene OptionSlot = GD.Load<PackedScene>("res://Scenes/choice_slot.tscn");
    public override void _Ready()
	{
		this.Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void closeChoices()
	{
		Hide();
		GetTree().Paused = false;
	}
    public void showChoices()
    {
        var optionSlot = OptionSlot.Instantiate();
        AddChild(optionSlot);
        Show();
        GetTree().Paused = true;
    }
}
