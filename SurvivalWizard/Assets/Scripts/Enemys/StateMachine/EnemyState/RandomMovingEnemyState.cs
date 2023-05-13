
using SurvivalWizard.Enemys;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace SurvivalWizard.EnemyState
{
    public class RandomMovingEnemyState : IStateEnemy
    {
        public event Action OnStartedMovingEvent;

        private const float _speedFactorForAnimator = 0.1f;

        private float _wanderRadius;
        private float _wanderTimer;

        private Transform _transform;
        private NavMeshAgent _agent;
        private Animator _animator;
        private Enemy _enemy;
        private float _timer;

        public RandomMovingEnemyState(Enemy enemy, float wanderRadius, float wanderTime)
        {
            _enemy = enemy;
            _transform = enemy.transform;
            _animator = enemy.Animator;
            _agent = enemy.NavMeshAgent;
            _wanderRadius = wanderRadius;
            _wanderTimer = wanderTime;
            
        }
        public void Enter()
        {
            _animator.SetFloat("Speed", 0);
        }

        public void Exit()
        {

        }

        public void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _wanderTimer)
            {
                _animator.SetFloat("Speed", _enemy.Speed * _speedFactorForAnimator);
                Vector3 newPos = RandomNavSphere(_transform.position, _wanderRadius, -1);
                _agent.SetDestination(newPos);
                _timer -= _wanderTimer;
                OnStartedMovingEvent?.Invoke();
            }
        }

        public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

            return navHit.position;
        }
    }
}