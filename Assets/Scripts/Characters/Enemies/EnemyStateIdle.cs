using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyStateIdle : IEnemyState
    {
        private EnemyBase _enemyBase;
        private Transform _transform;

        private float _agrRadius;

        public void Init(EnemyBase enemyBase, EnemyConfig config)
        {
            _enemyBase = enemyBase;
            _agrRadius = config.AgrRadiusIdle;
            _transform = enemyBase.transform;
        }

        public void OnActivate()
        {

        }

        public void OnUpdate()
        {
            if (Vector3.Distance(_transform.position, _enemyBase.PlayerTransform.position) < _agrRadius)
            {
                _enemyBase.SetState(EnemyState.Attack);
            }
        }

        public void OnDeactivate()
        {

        }
    }
}
