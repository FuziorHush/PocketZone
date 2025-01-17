using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemiesDetection : MonoBehaviour
{
    private LayerMask _circleCastLM;
    private LayerMask _overlapCircleLM;
    private int _enemyLayer;

    private float _detectionRadius;
    private float _circleCastRadius;

    public Transform TargetEnemy { get; private set; }

    public void Init() {
        _enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void Update()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _overlapCircleLM);
        if (enemies.Length > 0)
        {
            TargetEnemy = FindClosestEnemyInLineOfSight(enemies);
        }
        else 
        {
            TargetEnemy = null;
        }
    }

    private Transform FindClosestEnemyInLineOfSight(Collider2D[] enemies)
    {
        Collider2D closestCollider = null;
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < enemies.Length; i++)
        {
            Vector2 vec = transform.position - enemies[i].transform.position;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, _circleCastRadius, vec.normalized, vec.magnitude, _circleCastLM);//obstacles, enemies

            if (hit.transform != null && hit.transform.gameObject.layer == _enemyLayer)//if hits enemy
            {
                float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (distance < shortestDistance)
                {
                    closestCollider = enemies[i];
                    shortestDistance = distance;
                }
            }
        }

        return closestCollider.transform;
    }
}
