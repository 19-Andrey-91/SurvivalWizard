
using SurvivalWizard.EnemyState;
using UnityEngine;

namespace SurvivalWizard.Enemys
{
    public class MovingEnemyToTarget : Enemy
    {
        private EnemyMovingTargetState _enemyMovingTargetState;
        protected override void Start()
        {
            base.Start();
            _enemyMovingTargetState = new EnemyMovingTargetState(_agent, _target);
            _stateMachine.Initialize(_enemyMovingTargetState);
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }
    }
}
