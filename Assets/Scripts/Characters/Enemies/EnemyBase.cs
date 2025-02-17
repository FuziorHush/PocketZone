using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace Enemies
{
    public class EnemyBase : MonoBehaviour //make derived class for new enemy behavior
    {
        [SerializeField] private EnemyConfig _config;
        public EnemyConfig Config => _config;

        private Health _healthConponent;
        public NavMeshAgent NavMeshAgent { get; private set; }

        private Dictionary<EnemyState, IEnemyState> _states = new Dictionary<EnemyState, IEnemyState>();
        public EnemyState State { get; private set; }
        private IEnemyState _currentState;

        public Transform PlayerTransform { get; private set; }

        public void Init()
        {
            _healthConponent = GetComponent<Health>();
            NavMeshAgent = GetComponent<NavMeshAgent>();

            EnemyStateIdle enemyStateIdle = new EnemyStateIdle();
            enemyStateIdle.Init(this, _config);
            _states.Add(EnemyState.Idle, enemyStateIdle);

            EnemyStateAttack enemyStateAttack = new EnemyStateAttack();
            enemyStateAttack.Init(this, _config);
            _states.Add(EnemyState.Attack, enemyStateAttack);

            _healthConponent.Init(_config.Health);
            _healthConponent.HealthChanged += OnHealthChanged;

            NavMeshAgent.speed = _config.MoveSpeed;
            NavMeshAgent.stoppingDistance = _config.DamageRadius;

            PlayerTransform = SceneObjects.Instance.PlayerGameObject.transform;

            _currentState = _states[0];
            _currentState.OnActivate();
        }

        private void Update()
        {
            _currentState.OnUpdate();
        }

        private void OnHealthChanged(float health, float maxHealth)
        {
            if (health <= 0)
            {
                GameEvents.EnemyDied?.Invoke(gameObject);
                Destroy(gameObject);
            }
        }

        public bool SetState(EnemyState state)
        {
            if (_states.ContainsKey(state))
            {
                _currentState.OnDeactivate();

                _currentState = _states[state];
                _currentState.OnActivate();

                State = state;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
