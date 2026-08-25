using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Export] public float maxHealth = 10;
	private float health = 10; 
	
	public void takeDamage(float damage){
		health-=damage;
		if(health<=0){
			health = 0;
			//morte
		}
	}
	
	public void healHealth(float amount){
		health+=amount;
		if(health>maxHealth){
			health = maxHealth;
		}
	}
}
