using Godot;
using System;

public partial class ChoiceSlot : TextureButton
{
    private Weapon weapon;

    [Export] public Weapon Weapon
    {
        get => weapon;
        set
        {
            weapon = value;

            TextureNormal = value.Sprite;
            GetNode<Label>("Label").Text = "Lvl " + (weapon.level + 1);
        }
    }

    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void _OnClickChoice(InputEvent eventInput)
    {
        if(eventInput.IsActionPressed("click"))
        {
            // GD.Print(weapon.Nome);
            Choices parent = GetParent<Choices>();
            parent.closeChoices();
        }
    }
}
