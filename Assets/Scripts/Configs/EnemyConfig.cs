using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public float Health;
    public float MoveSpeed;
    public float Damage;
}
