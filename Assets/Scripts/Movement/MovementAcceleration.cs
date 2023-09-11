using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class MovementAcceleration : PlayerMovement
{
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float deacceleration;
    [SerializeField] private float speedMax = 10f;
    private Vector2 direction;
    private Vector2 velocity = Vector2.zero;

    public MovementAcceleration(MovementData md)
    {
        accelerationSpeed = md.accelerationSpeed;
        deacceleration = md.deacceleration;
        speedMax = md.speedMax;
    }

    public override void HandleMovement(Player player)
    {
        // Movement
        MovementInput();
        Accelerate();
        Deaccelerate();
        LimitSpeed();

        // Rotation
        base.RotationManager(player);

        // Apply Movement
        player.Rb.velocity = velocity;
        base.BindToScreenY(player);
        base.WrapScreenX(player);
        UpdatePlayerInertia(player);
    }

    public override void UpdatePlayerInertia(Player player)
    {
        player.inertia = velocity;
    }

    private void MovementInput()
    {
        direction = Vector2.zero;
        direction.x += Input.GetAxisRaw("Horizontal");
        direction.y += Input.GetAxisRaw("Vertical");
        direction.Normalize();
    }

    private void Accelerate()
    {
        velocity += direction * accelerationSpeed * Time.deltaTime;
    }

    private void Deaccelerate() 
    {
        // OBS: Swapping very quickly between opposite directions bypasses high deacceleration speeds

        if (direction.x == 0)
        {
            if (velocity.x > 0) velocity.x -= deacceleration * Time.deltaTime; 
            if (velocity.x < 0) velocity.x += deacceleration * Time.deltaTime;
        }

        if (direction.y == 0) { 
            if (velocity.y > 0) velocity.y -= deacceleration * Time.deltaTime;
            if (velocity.y < 0) velocity.y += deacceleration * Time.deltaTime;
        }
    }

    private void LimitSpeed()
    {
        velocity.x = Mathf.Clamp(velocity.x, -speedMax, speedMax);
        velocity.y = Mathf.Clamp(velocity.y, -speedMax, speedMax);
    }


}
