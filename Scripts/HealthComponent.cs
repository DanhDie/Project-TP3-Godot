using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Export] public float maxHealth = 10f;
	private float health;
	[Export] private bool canBeDamaged = true;
	[Export] private Timer invulFrames = null;
	[Export] private float invulTime = 0f;
    public override void _Ready()
	{
		health = maxHealth;
	}

	public void takeDamage(float damage){
		if (canBeDamaged)
		{
            health -= damage;
			invulFrames.Start();
			canBeDamaged = false;
            if (health <= 0)
            {
                health = 0;
                if (GetParent() is IDestrutivel destrutivel)
                {
                    destrutivel.destruirSe();
                }
            }
        }
	}
	
	public void healHealth(float amount){
		health+=amount;
		if(health>maxHealth){
			health = maxHealth;
		}
	}

	public float getHealth()
	{
		return health;
	}

	public void _whenInvulTimeOut()
	{
		canBeDamaged = true;
	}

}
