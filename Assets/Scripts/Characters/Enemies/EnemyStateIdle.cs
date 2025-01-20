using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyStateIdle : MonoBehaviour, IEnemyState
    {
        private EnemyBase _enemyBase;

        private float _agrRadius;

        public void Init(EnemyBase enemyBase, EnemyConfig config)
        {
            _enemyBase = enemyBase;
            _agrRadius = config.AgrRadiusIdle;
        }

        public void OnActivate()
        {

        }

        public void OnUpdate()
        {
            if (Vector3.Distance(transform.position, _enemyBase.PlayerTransform.position) < _agrRadius)
            {
                _enemyBase.SetState(EnemyState.Attack);
            }
        }

        public void OnDeactivate()
        {

        }
    }
}
