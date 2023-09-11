using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponData", order = 2)]
public class WeaponData : ScriptableObject
{
    [Header("Laser")]
    public GameObject laserPrefab;
    public float laserSpeed = 5f;
    public float coolDownMax = 0.1f;
}
