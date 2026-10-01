using Godot;
using System;

public partial class TelaPause : Control
{
    private CenterContainer options;
    public override void _Ready()
    {
        options = GetNode<CenterContainer>("Options");
        this.Hide();
    }
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("escape") && GetTree().Paused == true)
        {
            resume();
            
        }
        else if(Input.IsActionJustPressed("escape") && GetTree().Paused == false)
        {
            pause();
        }
    }

    private void resume()
    {
        GetTree().Paused = false;
        this.Hide();
    }
    private void pause()
    {
        GetTree().Paused = true;
        this.Show();
    }

    public void _OnResumePressed()
    {
        resume();
    }

    public void _OnRestartPressed()
    {
        GetTree().ReloadCurrentScene();
        resume();
    }


    public void _OnExitPressed()
    {
        TransicionScene transicao = GetNode<TransicionScene>("/root/TransicionScene");
        transicao.transicion("res://Scenes/tela_inicial.tscn");
        resume();
    }

    public void _OnOptionsPressed()
    {
        options.Show();
    }
}
