using Godot;
using System;

public partial class Choices : VBoxContainer
{
	[Export] HBoxContainer weapons;
    private PackedScene ChoiceSlot = GD.Load<PackedScene>("res://Scenes/choice_slot.tscn");
    public override void _Ready()
	{
		this.Hide();
	}

	public void closeChoices()
	{
		Hide();
		GetTree().Paused = false;
	}
    public void showChoices()
    {
        var choiceSlot = ChoiceSlot.Instantiate();
        AddChild(choiceSlot);
        Show();
        GetTree().Paused = true;
    }
}
