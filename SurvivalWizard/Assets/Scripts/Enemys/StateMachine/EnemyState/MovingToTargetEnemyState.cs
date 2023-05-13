
using SurvivalWizard.Enemys;
using UnityEngine;
using UnityEngine.AI;

namespace SurvivalWizard.EnemyState
{
    public class MovingToTargetEnemyState : IStateEnemy
    {
        private const float _timeBetweenSetDestination = 0.5f;
        private const float _speedFactorForAnimator = 0.1f;
        private Transform _target;
        private NavMeshAgent _agent;
        private Enemy _enemy;
        private Animator _animator;
        private float _timer;

        public MovingToTargetEnemyState(Enemy enemy, Transform target)
        {
            _animator = enemy.Animator;
            _target = target;
            _agent = enemy.NavMeshAgent;
            _enemy = enemy;
        }
        public void Enter()
        {
            _animator.SetFloat("Speed", _enemy.Speed * _speedFactorForAnimator);
        }

        public void Exit()
        {
            _animator.SetFloat("Speed", 0f);
        }

        public void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _timeBetweenSetDestination)
            {
                _agent.SetDestination(_target.position);
                _timer -= _timeBetweenSetDestination;
            }
        }
    }
}
