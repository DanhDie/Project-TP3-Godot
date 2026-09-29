using Godot;

public partial class DisplayNumberManager : Node
{
    private Theme damageTheme;

    public override void _Ready()
    {
        damageTheme = GD.Load<Theme>("res://Theme/theme_morax.tres");
    }
    public async void displayNumber(float damage, Vector2 position)
    {
        Godot.Label number = new Godot.Label();
        number.Theme = damageTheme;
        number.Text = damage.ToString();
        number.GlobalPosition = position;
        number.ZIndex = 5;

        AddChild(number);

        // Dá um frame para o Label calcular seu tamanho
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        number.PivotOffset = number.Size / 2;

        Tween tween = GetTree().CreateTween();

        tween.SetParallel(true);

        tween.TweenProperty(
            number,
            "position:y",
            number.Position.Y - 24,
            0.25
        ).SetEase(Tween.EaseType.Out);

        tween.TweenProperty(
            number,
            "position:y",
            number.Position.Y,
            0.5
        ).SetEase(Tween.EaseType.In)
        .SetDelay(0.25);

        tween.TweenProperty(
            number,
            "scale",
            Vector2.Zero,
            0.25
        ).SetEase(Tween.EaseType.In)
        .SetDelay(0.5);

        await ToSignal(tween, Tween.SignalName.Finished);

        number.QueueFree();
    }
}