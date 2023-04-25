using UnityEngine;
using UnityEngine.AI;

namespace SurvivalWizard.EnemyState
{
    public class EnemyRandomMovingState : StateEnemy
    {
        private float _wanderRadius;
        private float _wanderTimer;

        private Transform _transform;
        private NavMeshAgent _agent;
        private float _timer;

        public EnemyRandomMovingState(Transform position, NavMeshAgent agent, float wanderRadius, float wanderTime)
        {
            _transform = position;
            _agent = agent;
            _wanderRadius = wanderRadius;
            _wanderTimer = wanderTime;
        }
        public override void Enter()
        {
            _timer = _wanderTimer;
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(_transform.position, _wanderRadius, -1);
                _agent.SetDestination(newPos);
                _timer -= _wanderTimer;
            }
        }

        public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

            return navHit.position;
        }
    }
}