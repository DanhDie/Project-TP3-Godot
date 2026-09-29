using Godot;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

public partial class EnemySpawner : Node2D
{
	[Export] private Personagem player;
	[Export] private PackedScene enemy;

    GameManager GameManager;

	private float distance = 400f;
    private bool canSpawn = true;
    private int seconds;
    public override void _Ready()
    {
    }
    public override void _PhysicsProcess(double delta)
    {
        if (GetTree().GetNodeCountInGroup("EnemyBasic") > 700)
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }
    }


    private void spawnEnemy(Vector2 position)
	{
        var enemyInstance = enemy.Instantiate() as Inimigo;

        enemyInstance.Position = position;
        enemyInstance.player = player;

        GetTree().CurrentScene.AddChild(enemyInstance);
    }

    private Vector2 getRandomPos()
    {
        float angle = GD.Randf() * Mathf.Tau;
        return player.Position + distance * Vector2.Right.Rotated(angle);
    }

    private void amountSpawn(int n = 1)
    {
        if (canSpawn)
        {
            for (int i = 0; i < n; i++)
            {
                spawnEnemy(getRandomPos());
            }
        }
    }

    public void _OnTimeOut()
    {
        seconds++;
        amountSpawn(seconds % 10);
    }
}
