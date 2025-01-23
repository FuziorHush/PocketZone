using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemiesDetection : IPlayerContextUpdate
{
    private LayerMask _circleCastLM;
    private LayerMask _overlapCircleLM;
    private Transform _shootPoint;
    private int _enemyLayer;

    private float _detectionRadius;
    private float _circleCastRadius = 0.1f;

    public Transform TargetEnemy { get; private set; }

    public void Init(PlayerConfig config, LayerMask circleCastLM, LayerMask overlapCircleLM, Transform shootPoint) 
    {
        _enemyLayer = LayerMask.NameToLayer("Enemies");
        _circleCastLM = circleCastLM;
        _overlapCircleLM = overlapCircleLM;
        _shootPoint = shootPoint;
        _detectionRadius = config.EnemyDetectionRadius;
    }

    public void OnUpdate() 
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_shootPoint.position, _detectionRadius, _overlapCircleLM);
        if (enemies.Length > 0)
        {
            Collider2D closest = FindClosestEnemyInLineOfSight(enemies);

            if (closest == null)
                TargetEnemy = null;
            else
                TargetEnemy = closest.transform;
        }
        else
        {
            TargetEnemy = null;
        }
    }

    private Collider2D FindClosestEnemyInLineOfSight(Collider2D[] enemies)
    {
        Collider2D closestCollider = null;
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < enemies.Length; i++)
        {
            Vector2 vec = enemies[i].transform.position - _shootPoint.position;
            float distance = vec.magnitude;
            RaycastHit2D hit = Physics2D.CircleCast(_shootPoint.position, _circleCastRadius, vec.normalized, distance, _circleCastLM);//obstacles, enemies
            if (hit.transform != null && hit.transform.gameObject.layer == _enemyLayer)//if hits enemy
            {
                if (distance < shortestDistance)
                {
                    closestCollider = enemies[i];
                    shortestDistance = distance;
                }
            }
        }

        return closestCollider;
    }
}
