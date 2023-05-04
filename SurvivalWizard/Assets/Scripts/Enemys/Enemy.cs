
using SurvivalWizard.Base;
using SurvivalWizard.PlayerScripts;
using UnityEngine;
using UnityEngine.AI;

namespace SurvivalWizard.Enemys
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : Entity
    {
        protected StateMachineEnemy _stateMachine;
        protected NavMeshAgent _agent;
        protected Transform _target;

        private int _numberPool = -1;

        public int NumberPool
        {
            get => _numberPool;
            set
            {
                if (_numberPool >= 0)
                {
                    throw new UnityException("NumberPool already set");
                }
                if (value < 0)
                {
                    throw new UnityException("NumberPool cannot be less than 0");
                }
                _numberPool = value;
            }
        }
        private Transform _enemyTransform;

        public Transform EnemyTransform { get => _enemyTransform; }

        private void Awake()
        {
            _enemyTransform = transform;
            _agent = GetComponent<NavMeshAgent>();
            _stateMachine = new StateMachineEnemy();
            _agent.speed = _speed;
        }

        protected virtual void Start()
        {
            _target = GameManager.Instance.Player.transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.TakeDamage(Damage);
            }
        }
    }
}
