using Godot;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

public partial class EnemySpawner : Node2D
{
	[Export] private Personagem player;
	[Export] private PackedScene enemy;
	[Export] private int spawnFreq;
    [Export] private Godot.Collections.Array<PackedScene> enemies;
    [Export] private Godot.Collections.Array<PackedScene> elites;
    [Export] private Godot.Collections.Array<PackedScene> bosses;
    private int indexEnemy = 0;
    private int waveCounter = 0;
    GameManager GameManager;
	private float distance = 450f;
    private bool canSpawn = true;
    private int seconds;
    public override void _Ready()
    {
        indexEnemy = GD.RandRange(0, enemies.Count - 1);
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


    //    private void spawnEnemy(Vector2 position)
    //	{
    //        var enemyInstance = enemy.Instantiate() as Inimigo;
    //
    //       enemyInstance.Position = position;
    //        enemyInstance.player = player;
    //
    //        GetTree().CurrentScene.AddChild(enemyInstance);
    //    }

    private void spawnEnemy(Vector2 position, Godot.Collections.Array<PackedScene> enemyList, int index)
    {
        if (enemyList.Count == 0)
            return;
        if (index >= enemyList.Count)
            index = 0;

        PackedScene enemyScene = enemyList[index];

        var enemyInstance = enemyScene.Instantiate() as Inimigo;

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
                spawnEnemy(getRandomPos(),enemies, indexEnemy);
            }
        }
    }

    public void _OnTimeOut()
    {
        seconds++;
        amountSpawn(seconds % spawnFreq+1);
    }

    public void _OnPatternTimeoutEnemyChange()
    {
        indexEnemy = GD.RandRange(0, enemies.Count - 1);
    }

    public void _OnPatternTimeoutCircle()
    {
        if (!canSpawn || enemies.Count == 0)
            return;

        int amount = 5+spawnFreq*10;
        float radius = 350f;

        indexEnemy = GD.RandRange(0, enemies.Count - 1);

        for (int i = 0; i < amount; i++)
        {
            float angle = Mathf.Tau * i / amount;

            Vector2 position = player.Position +
                               Vector2.Right.Rotated(angle) * radius;

            spawnEnemy(position, enemies, indexEnemy);
        }
    }

    public void _OnEliteTimeOutUpFrequency()
    {
        waveCounter++;
        if (waveCounter % 2 == 0)
        {
            spawnFreq++;
        }
    }

    public void _OnEliteTimeOutSpawn()
    {
        spawnEnemy(getRandomPos(), elites, GD.RandRange(0, elites.Count - 1));
    }

    public void _OnBossTimeOutSpawn()
    {
        spawnEnemy(getRandomPos(), bosses, waveCounter);
    }

}
