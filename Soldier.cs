using Godot;
using System;
using System.Runtime.CompilerServices;

public class Soldier : CSGBox
{
    public const int CharactersPerShot = 1;
    public const int DamagePerShot = 1;
    public Zombie Target;

    private int charctersStored;

    public override void _Ready()
    {
        Width = .2f;
        Height = .4f;
        Depth = .2f;
        AddToGroup("soldiers");
    }

    public void OnCharacterComplete()
    {
        charctersStored++;
        if (charctersStored >= CharactersPerShot)
        {
            charctersStored -= CharactersPerShot;
            Fire();
        }
    }

    private void FindTarget()
    {
        var zombies = GetTree().GetNodesInGroup("zombies");
        if (zombies.Count == 0)
        {
            return;
        }

        var closest = zombies[0] as Zombie;
        var closestDistance = (closest.Transform.origin - Transform.origin).LengthSquared();
        foreach (Zombie zombie in zombies)
        {
            var distance = (zombie.Transform.origin - Transform.origin).LengthSquared();
            if (distance < closestDistance)
            {
                closest = zombie;
                closestDistance = distance;
            }
        }

        Target = closest as Zombie;
    }

    private void Fire()
    {
        if (!IsInstanceValid(Target))
        {
            Target = null;
        }

        if (Target == null)
        {
            FindTarget();
            if (Target == null)
            {
                return;
            }
        }

        GD.Print("Pew! " + DamagePerShot + " damage to " + Target + " who has " + Target.Health + " health left.");
        Target.TakeDamage(DamagePerShot);
    }
}
