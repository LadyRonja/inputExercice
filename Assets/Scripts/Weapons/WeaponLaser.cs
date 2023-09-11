using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponLaser : Weapon
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private List<Transform> firePoints = new();
    private int firePointItterator = 0;
    [SerializeField] private float projectileSpeed = 1f;

    public WeaponLaser(WeaponData wd, List<Transform> barrelPoints)
    {
        laserPrefab = wd.laserPrefab;
        firePoints = barrelPoints;
        projectileSpeed = wd.laserSpeed;
        cooldownUseMax = wd.coolDownMax;
    }
    public override void WeaponManager(Player player)
    {
        base.CooldownManager();
        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            AttemptFire(player);
        }
    }

    private void AttemptFire(Player player)
    {
        if (canUse && firePoints.Count != 0)
        {
            Fire(player);
        }

    }
    private void Fire(Player player)
    {
        canUse = false;
        cooldownUseCur = cooldownUseMax;
        firePointItterator++;
        if (firePointItterator >= firePoints.Count)
            firePointItterator = 0;

        GameObject laserObj = Instantiate(laserPrefab, firePoints[firePointItterator].position, Quaternion.identity);
        laserObj.transform.rotation = player.transform.rotation;

        Laser laser = laserObj.GetComponent<Laser>();
        laser.moveSpeed = projectileSpeed;

    }
}
