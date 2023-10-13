using Godot;
using System;

public class Zombie : CSGBox
{
    public const float MovementSpeed = 1.0f;

    public override void _Ready()
    {
        Width = .2f;
        Height = .3f;
        Depth = .2f;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (Transform.origin.x < 1)
        {
            return;
        }

        var translation = new Vector3(-delta * MovementSpeed, 0, 0);
        TranslateObjectLocal(translation);
    }
}
