using Godot;
using System;

public partial class TransicionScene : CanvasLayer
{
    private ColorRect colorRect;
    private Godot.AnimationPlayer animationPlayerCena;

    private string proximaCena = "";

    public override void _Ready()
    {
        colorRect = GetNode<ColorRect>("ColorRect");
        animationPlayerCena = GetNode<Godot.AnimationPlayer>("AnimationPlayer");

        colorRect.Visible = false;

        animationPlayerCena.AnimationFinished += AoTerminarAnimacao;
    }

    private void AoTerminarAnimacao(StringName animName)
    {
        if (animName == "FadeToBlack" && proximaCena != "")
        {
            GetTree().ChangeSceneToFile(proximaCena);
            animationPlayerCena.Play("FadeToNormal");
        }
        else if (animName == "FadeToNormal")
        {
            colorRect.Visible = false;
        }
    }

    public void transicion(string cena)
    {
        proximaCena = cena;

        colorRect.Visible = true;

        animationPlayerCena.Play("FadeToBlack");
    }
}