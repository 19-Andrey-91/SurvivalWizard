
using UnityEngine;

namespace SurvivalWizard.Enemys
{
    public class RandomMovingEnemy : Enemy
    {
        [Header("RandomMovingPoint")]
        [SerializeField] private float _wanderRadius;
        [SerializeField] private float _wanderTimer;

        private EnemyRandomMovingState _enemyMovingState;

        protected override void Start()
        {
            base.Start();
            _enemyMovingState = new EnemyRandomMovingState(transform, _agent, _wanderRadius, _wanderTimer);
            _stateMachine.Initialize(_enemyMovingState);
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }
    }
}