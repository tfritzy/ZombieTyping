using Godot;
using System;

public class ZombieSpawner : Node
{
	public float SpawnInterval = 1.0f;
	private float LastSpawnTime = 0.0f;
	private Spatial Zombie;

	public override void _Ready()
	{
		Zombie = GD.Load<PackedScene>("res://Assets/Zombies/Zombie.tscn").Instance() as Spatial;
	}

	public override void _Process(float delta)
	{
		LastSpawnTime += delta;
		if (LastSpawnTime > SpawnInterval)
		{
			LastSpawnTime = 0.0f;
			var zombie = Zombie.Duplicate() as Spatial;
			GetTree().Root.AddChild(zombie);

			float z = (float)GD.RandRange(-5, 5);
			zombie.Transform = new Transform(zombie.Transform.basis, new Vector3(20, 0, z));
		}
	}
}
