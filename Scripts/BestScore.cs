using Godot;
using System;

public partial class BestScore : VBoxContainer
{
    GameManager gameManager;
    private RichTextLabel score;
    public override void _Ready()
    {
        gameManager = GetNode<GameManager>("/root/GameManager");
        score = GetNode<RichTextLabel>("Pontuacao");
        if (gameManager.currentData.bestScore > 0)
        {
            score.Text = "[wave]" + gameManager.currentData.bestScore;
            Show();
        }
        else
        {
            Hide();
        }
    }
}
