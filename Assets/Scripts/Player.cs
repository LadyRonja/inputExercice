using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movementManager;
    [HideInInspector] public Vector2 inertia;

    public enum MovementTypes { Velocity, Acceleration, Gravity};
    [SerializeField] private MovementTypes movementType;
    private MovementTypes lastFramesMoveSystem;

    [SerializeField] private MovementData movementData;
    [Space]
    public Transform WrapCloneLeft;
    public Transform WrapCloneRight;

    private void Start()
    {
        SetUpMoveSystem();
    }


    private void Update()
    {
        movementManager.HandleMovement(this);
        CheckMoveSystemChange();
        if (Input.GetKeyDown(KeyCode.G))
            ToggleGravity();
    }

    private void ToggleGravity()
    {
        if (movementType != MovementTypes.Gravity) 
            movementType = MovementTypes.Gravity;
        else 
            movementType = MovementTypes.Acceleration;
    }
  
    private void SetUpMoveSystem()
    {
        switch (movementType)
        {
            case MovementTypes.Velocity:
                movementManager = new MovementVelocity(movementData);
                lastFramesMoveSystem = MovementTypes.Velocity;
                break;
            case MovementTypes.Acceleration:
                movementManager = new MovementAcceleration(movementData);
                lastFramesMoveSystem = MovementTypes.Acceleration;
                break;
            case MovementTypes.Gravity:
                movementManager = new MovementGravity(movementData, inertia);
                lastFramesMoveSystem = MovementTypes.Gravity;
                break;
            default:
                Debug.LogError("End of Switch-State-Machine reaced, new state not added?");
                movementManager = new MovementVelocity(movementData);
                lastFramesMoveSystem = MovementTypes.Velocity;
                break;
        }
    }
    private void CheckMoveSystemChange()
    {
        if (lastFramesMoveSystem == movementType) return;
        SetUpMoveSystem();
    }
}
