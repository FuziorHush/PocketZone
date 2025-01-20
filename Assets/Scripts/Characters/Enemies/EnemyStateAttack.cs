using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateAttack : MonoBehaviour, IEnemyState
{
    private EnemyBase _enemyBase;
    private NavMeshAgent _navMeshAgent;

     private float _agrRadius;
     private float _damageRadius;
     private float _damage;
     private float _damageDelay;

    private float _currentDamageDelay;

    public void Init(EnemyBase enemyBase, EnemyConfig config)
    {
        _enemyBase = enemyBase;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _agrRadius = config.AgrRadiusIdle;
        _damage = config.Damage;
        _damageDelay = config.DamageDelay;
        _damageRadius = config.DamageRadius;
    }

    public void OnActivate()
    {

    }

    public void OnUpdate()
    {
        if (_currentDamageDelay > 0)
        {
            _currentDamageDelay -= Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, _enemyBase.PlayerTransform.position) < _damageRadius && _currentDamageDelay <= 0)
        {
            _enemyBase.PlayerTransform.GetComponent<Health>().TakeDamage(_damage);
            _currentDamageDelay += _damageDelay;
        }
        else if (Vector3.Distance(transform.position, _enemyBase.PlayerTransform.position) > _agrRadius)
        {
            _enemyBase.SetState(EnemyState.Idle);
        }
        else {
            _navMeshAgent.SetDestination(_enemyBase.PlayerTransform.position);
        }
    }

    public void OnDeactivate()
    {
        _currentDamageDelay = 0;
    }
}
