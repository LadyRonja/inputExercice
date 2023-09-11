using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    [Header("Movement")]
    [SerializeField] private MovementData movementData;
    private Rigidbody2D rb;
    private PlayerMovement movementManager;
    public enum MovementTypes { Velocity, Acceleration, Gravity};
    [SerializeField] private MovementTypes movementType;
    private MovementTypes lastFramesMoveSystem;
    [HideInInspector] public Vector2 inertia;

    [Space]
    public Transform WrapCloneLeft;
    public Transform WrapCloneRight;

    [Header("Weapons")]
    [SerializeField] private WeaponData weaponData;
    private Weapon activeWeapon;
    public enum WeaponTypes { Laser};
    [SerializeField] private WeaponTypes weaponType;
    private WeaponTypes lastFramesWeaponSystem;
    public List<Transform> laserFirePoints = new();

    public Rigidbody2D Rb { get => rb; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetUpMoveSystem();
        SetUpWeaponSystem();
    }


    private void Update()
    {
        activeWeapon.WeaponManager(this);
        CheckMoveSystemChange();
        CheckWeaponSystemChange();
        if (Input.GetKeyDown(KeyCode.G))
            ToggleGravity();
    }

    private void FixedUpdate()
    {
        movementManager.HandleMovement(this);
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

    private void SetUpWeaponSystem()
    {
        switch (weaponType)
        {
            case WeaponTypes.Laser:

                lastFramesWeaponSystem = WeaponTypes.Laser;
                activeWeapon = new WeaponLaser(weaponData, laserFirePoints);
                break;
            default:
                Debug.LogError("End of Switch-State-Machine reaced, new state not added?");
                activeWeapon = new WeaponLaser(weaponData, laserFirePoints);
                lastFramesWeaponSystem = WeaponTypes.Laser;
                break;
        }
    }

    private void CheckMoveSystemChange()
    {
        if (lastFramesMoveSystem == movementType) return;
        SetUpMoveSystem();
    }

    private void CheckWeaponSystemChange()
    {
        if (lastFramesWeaponSystem == weaponType) return;
        SetUpWeaponSystem();
    }
}
