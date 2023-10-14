using Godot;
using System;

public class Zombie : Spatial
{
	public const float MovementSpeed = 1.0f;
	public int Health { get; private set; }

	public override void _Ready()
	{
		Health = 5;
		AddToGroup("zombies");
		RotationDegrees = new Vector3(0, 0, 0);
	}

	public override void _Process(float delta)
	{
		base._Process(delta);

		if (Transform.origin.x < 1)
		{
			return;
		}

		// Walk forward
		var translation = Vector3.Left * MovementSpeed * delta;
		Transform = Transform.Translated(translation);
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;
		if (Health <= 0)
		{
			GD.Print("GHEIUAH");
			QueueFree();
		}

		MakeBloodSplatter();
	}

	private void MakeBloodSplatter()
	{
		var bloodSplat =
					GD.Load<PackedScene>("res://Assets/ParticleSystems/BloodSplat.tscn").Instance() as CPUParticles;
		GetParent().AddChild(bloodSplat);
		bloodSplat.GlobalTransform = GlobalTransform;

		bloodSplat.Emitting = true;
		foreach (Node child in bloodSplat.GetChildren())
		{
			if (child is CPUParticles particles)
			{
				particles.Emitting = true;
			}
		}

		var timer = new Timer();
		bloodSplat.AddChild(timer);


		timer.Connect("timeout", bloodSplat, "queue_free");
		timer.WaitTime = 2f;
		timer.Start();
	}
}
