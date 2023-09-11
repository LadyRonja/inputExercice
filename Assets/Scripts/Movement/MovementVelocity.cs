using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVelocity : PlayerMovement
{
    [SerializeField] private float speed = 5f;
    private Vector2 direction;

    public MovementVelocity(MovementData md)
    {
        speed = md.speed;
    }

    public override void HandleMovement(Player player)
    {
        MovementInput();

        // Rotation
        base.RotationManager(player);

        // Apply Movement
        player.Rb.velocity = (Vector3)direction * speed /** Time.deltaTime*/;
        base.BindToScreenY(player);
        base.WrapScreenX(player);
        UpdatePlayerInertia(player);
    }

    public override void UpdatePlayerInertia(Player player)
    {
        player.inertia = direction * speed;
    }

    private void MovementInput()
    {
        direction = Vector2.zero;
        direction.x += Input.GetAxisRaw("Horizontal");
        direction.y += Input.GetAxisRaw("Vertical");
        direction.Normalize();
    }


}
