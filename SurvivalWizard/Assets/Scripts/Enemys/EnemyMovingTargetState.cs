
using UnityEngine;
using UnityEngine.AI;

namespace SurvivalWizard.Enemys
{
    public class EnemyMovingTargetState : State
    {
        private const float _timeBetweenSetDestination = 0.5f;
        private Transform _target;
        private NavMeshAgent _agent;
        private float _timer;

        public EnemyMovingTargetState(NavMeshAgent agent, Transform target)
        {
            _target = target;
            _agent = agent;
        }
        public override void Enter()
        {

        }

        public override void Exit()
        {

        }

        public override void Update()
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
