using Godot;
using System;

public partial class GameManager : Node
{
	public float timer = 0f;
	public int pontuacaoMortes = 0;

	public UserSaveData currentData;
    public override void _Ready()
	{
		currentData = SaveManager.loadGame();
	}


    public override void _Process(double delta)
	{
		aumentarTempo(delta);
	}

	private void aumentarTempo(double delta)
	{
		timer += (float)delta;
	}

	public void aumentarPontuacao(int number)
	{
		pontuacaoMortes += number;
	}

}
