using Godot;
using System;

public partial class TelaMorte : Control
{
    GameManager gameManager;
    [Export] private RichTextLabel score;
    public override void _Ready()
    {
        this.Hide();
        gameManager = GetNode<GameManager>("/root/GameManager");
        Personagem player = GetTree().CurrentScene.GetNode<Personagem>("Personagem");
        player.PlayerMorreu += MostrarTelaMorte;
    }

    private void MostrarTelaMorte()
    {
        int scoreValue = Mathf.FloorToInt(gameManager.timer)+ gameManager.pontuacaoMortes;
        if (scoreValue >= 100)
        {
            score.Text = "[wave]" + scoreValue;
        }
        else
        {
            score.Text = ""+ scoreValue;
        }
        this.Show();
    }
    private void resume()
    {
        GetTree().Paused = false;
        this.Hide();
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

}
