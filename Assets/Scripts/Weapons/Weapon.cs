using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Cooldown / Firerate")]
    [SerializeField] protected float cooldownUseMax = 0.1f;
    protected float cooldownUseCur = 0.1f;
    protected bool canUse = true;

    public abstract void WeaponManager(Player player);

    protected void CooldownManager()
    {
        if (cooldownUseCur <= 0f)
            canUse = true;
        else 
            cooldownUseCur -= Time.deltaTime;
    }
}
