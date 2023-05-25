
using SurvivalWizard.EnemyState;
using UnityEngine;

namespace SurvivalWizard.Enemies
{
    public class SimpleSlime : Enemy
    {
        private MovingToTargetEnemyState _movingTargetEnemyState;
        protected override void Start()
        {
            base.Start();
            _movingTargetEnemyState = new MovingToTargetEnemyState(this, _target.transform);
            _stateMachine.Initialize(_movingTargetEnemyState);
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }
    }
}
