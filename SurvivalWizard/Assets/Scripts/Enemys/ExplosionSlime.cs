
using SurvivalWizard.EnemyState;
using System;
using UnityEngine;

namespace SurvivalWizard.Enemys
{
    public class ExplosionSlime : Enemy
    {
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionDamage;

        private MovingToTargetEnemyState _movingTargetEnemyState;
        private ExplosionEnemyState _explosionEnemyState;

        protected override void Start()
        {
            base.Start();
            _movingTargetEnemyState = new MovingToTargetEnemyState(this, _target.transform);
            _explosionEnemyState = new ExplosionEnemyState(this, _target, _explosionDamage, _explosionRadius);

            _stateMachine.Initialize(_movingTargetEnemyState);
            Subscribe();
        }

        private void Subscribe()
        {
            _explosionEnemyState.OnExplodedEvent += ChangeStateToMovingToTarget;
            OnCausedDamage += ChangeStateToExplosion;
        }

        private void Unsubscribe()
        {
            _explosionEnemyState.OnExplodedEvent -= ChangeStateToMovingToTarget;
            OnCausedDamage -= ChangeStateToExplosion;
        }

        private void ChangeStateToMovingToTarget()
        {
            _stateMachine.ChangeState(_movingTargetEnemyState);
        }

        private void ChangeStateToExplosion()
        {
            _stateMachine.ChangeState(_explosionEnemyState);
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
