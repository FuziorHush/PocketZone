using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public float Health;

    public float MoveSpeed;
    public float Acceleration;
    public float MoveVectorDecreace;

    public float ItemsPickupRadius;
    public float EnemyDetectionRadius;
    public WeaponConfig WeaponConfig;
}
