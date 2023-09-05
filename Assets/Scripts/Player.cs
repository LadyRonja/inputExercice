using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movementManager;
    public enum MovementTypes { Velocity, Acceleration};
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
    }

    private void CheckMoveSystemChange()
    {
        if (lastFramesMoveSystem == movementType) return;
        SetUpMoveSystem();
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
            default:
                Debug.LogError("End of Switch-State-Machine reaced, new state not added?");
                movementManager = new MovementVelocity(movementData);
                lastFramesMoveSystem = MovementTypes.Velocity;
                break;
        }
    }
}
