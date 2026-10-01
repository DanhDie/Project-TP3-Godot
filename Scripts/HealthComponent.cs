using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Export] public float maxHealth = 10f;
	private float health;
	[Export] private bool canBeDamaged = true;
	[Export] private Timer invulFrames = null;
	[Export] private float invulTime = 0f;

	[Export] private Godot.AnimationPlayer hitAnimator;

	DisplayNumberManager displayNumber;

    private AudioStreamPlayer damageSound;
    public override void _Ready()
    {
        displayNumber = GetNode<DisplayNumberManager>("/root/DisplayNumberManager");
        //hitAnimator.Play("hitFlash");
        health = maxHealth;
        damageSound = GetNode<AudioStreamPlayer>("DamageSound");

    }

    public async void takeDamage(float damage){
		if (canBeDamaged)
		{
            if (invulFrames != null)
            {
                invulFrames.Start();
                canBeDamaged = false;
            }
            displayNumber.displayNumber(damage, this.GlobalPosition);
			hitAnimator.Play("hitFlash");
            health -= damage;
			damageSound.PitchScale = (float)GD.RandRange(.8, 1.2);
            damageSound.Play();
			await ToSignal(damageSound, AudioStreamPlayer.SignalName.Finished);
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
