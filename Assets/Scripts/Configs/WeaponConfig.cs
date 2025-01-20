using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public float Shootdelay;
    public float BulletDamage;
    public float BulletSpeed;
    public float Distance;
    public int Ammo;
    public PoolConfig BulletsPoolConfig;
}
