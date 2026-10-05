using System;
using Godot;

public class PlayerController
{
    private float _speed;

    public PlayerController(float speed)
    {
        _speed = speed;
    }

    public Vector2 GetVelocity()
    {
        var movementVector = GetMovementVector();
        var direction = movementVector.Normalized();
        return direction * _speed;
    }

    private Vector2 GetMovementVector()
    {   
        var xMovement = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
	    var yMovement = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

        return new Vector2(xMovement, yMovement);
    }
}