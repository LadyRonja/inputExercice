using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MovementData", order = 1)]
public class MovementData : ScriptableObject
{
    [Header("Velocity movement")]
    public float speed = 5f;

    [Header("Acceleration movement")]
    public float accelerationSpeed = 15f;
    public float deacceleration = 10f;
    public float speedMax = 7f;

    [Header("Gravity")]
    public float gravity = 5f;
    [Range(0, 1)]public float bounceEnergyConsumption = 0.8f;
}
