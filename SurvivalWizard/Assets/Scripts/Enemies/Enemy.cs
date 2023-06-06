
using SurvivalWizard.Base;
using SurvivalWizard.PlayerScripts;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace SurvivalWizard.Enemies
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class Enemy : Entity
    {
        public event Action OnCausedDamage;
        [Header("Resources")]
        [SerializeField] private int _numberOfExperience;
        [SerializeField] private int _numberOfCoins;

        protected StateMachineEnemy _stateMachine;
        protected NavMeshAgent _agent;
        protected Player _target;

        private Animator _animator;
        private int _numberPool = -1;

        public int NumberOfCoins { get => _numberOfCoins; }
        public int NumberOfExperience { get => _numberOfExperience; }
        public NavMeshAgent NavMeshAgent { get => _agent; }
        public Animator Animator { get => _animator; }

        public Player Target { get => _target; }

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

        public void Initialize(Player player)
        {
            _target = player;
        }

        private void Awake()
        {
            _enemyTransform = transform;
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _stateMachine = new StateMachineEnemy();
            _agent.speed = _speed;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (_stateMachine.CurrentState != null)
            {
                _stateMachine.ChangeState(_stateMachine.CurrentState);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                OnCausedDamage?.Invoke();
                player.TakeDamage(Damage);
            }
        }
    }
}
