using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Export] public float maxHealth = 10f;
	private float health;
	public override void _Ready()
	{
		health = maxHealth;
	}

	public void takeDamage(float damage){
		health-=damage;
		if(health<=0){
			health = 0;
			GetParent().QueueFree();
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
}
