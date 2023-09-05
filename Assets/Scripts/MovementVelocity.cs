using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVelocity : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;

    void Update()
    {
        MovementInput();

        // Apply Movement
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void MovementInput()
    {
        direction = Vector2.zero;
        direction.x += Input.GetAxisRaw("Horizontal");
        direction.y += Input.GetAxisRaw("Vertical");
        direction.Normalize();
    }
}
