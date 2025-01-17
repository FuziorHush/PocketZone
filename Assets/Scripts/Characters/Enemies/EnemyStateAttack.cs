using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAttack : MonoBehaviour, IEnemyState
{
    private EnemyBase _enemyBase;

    [SerializeField] private float _agrRadius;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _damage;
    [SerializeField] private float _damageDelay;

    private float _currentDamageDelay;

    public void Init(EnemyBase enemyBase, EnemyConfig config)
    {
        _enemyBase = enemyBase;
        _agrRadius = config.AgrRadiusIdle;
        _damage = config.Damage;
        _damageDelay = config.DamageDelay;
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

        if (Vector3.Distance(transform.position, _enemyBase.PlayerTransform.position) < _attackRadius && _currentDamageDelay <= 0)
        {
            _enemyBase.PlayerTransform.GetComponent<Health>().TakeDamage(_damage);
            _currentDamageDelay += _damageDelay;
        }
        else if (Vector3.Distance(transform.position, _enemyBase.PlayerTransform.position) > _agrRadius)
        {
            _enemyBase.SetState(EnemyState.Idle);
        }
    }

    public void OnDeactivate()
    {
        _currentDamageDelay = 0;
    }
}
