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

        // Apply Movement
        player.transform.position += (Vector3)direction * speed * Time.deltaTime;
        base.BindToScreenY(player);
        base.WrapScreenX(player);
    }


    private void MovementInput()
    {
        direction = Vector2.zero;
        direction.x += Input.GetAxisRaw("Horizontal");
        direction.y += Input.GetAxisRaw("Vertical");
        direction.Normalize();
    }


}
