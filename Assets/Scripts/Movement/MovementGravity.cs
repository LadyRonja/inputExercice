using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGravity : PlayerMovement
{
    [SerializeField] private float gravityForce = 5f;
    [SerializeField] private float boucneEnergyReduction = 0.8f;
    private Vector2 velocity = Vector2.zero;
    public MovementGravity(MovementData md)
    {
        gravityForce = md.gravity;
        boucneEnergyReduction = md.bounceEnergyConsumption;
        velocity = Vector2.zero;
    }
    public MovementGravity(MovementData md, Vector2 inertia)
    {
        gravityForce = md.gravity;
        boucneEnergyReduction = md.bounceEnergyConsumption;
        velocity = inertia;
    }

    public override void HandleMovement(Player player)
    {
        base.RotationManager(player);
       
        base.BindToScreenY(player);
        base.WrapScreenX(player);

        ApplyGravity(player);
        DeaccelerateXVelocity();
        //BounceOnGround(player);

        UpdatePlayerInertia(player);
    }

    public override void UpdatePlayerInertia(Player player)
    {
        player.inertia = velocity;
    }

    private void ApplyGravity(Player player)
    {
        velocity.y -= gravityForce * Time.deltaTime;
        player.Rb.velocity += velocity * Time.deltaTime;
    }

    private void DeaccelerateXVelocity()
    {
        if (Mathf.Abs(velocity.x) > 0.1f) 
            velocity.x -= velocity.x * 0.7f * Time.deltaTime;
        else 
            velocity.x = 0;
    }

    private void BounceOnGround(Player player)
    {
        float camPosY = cam.transform.position.y;
        float halfWolrdHeight = worldHeight / 2;

        if (player.transform.position.y <= camPosY - halfWolrdHeight)
        {
            velocity.y *= (1 - boucneEnergyReduction) * -1;
        }
    }
}
