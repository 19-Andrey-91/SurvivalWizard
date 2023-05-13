
using SurvivalWizard.EnemyState;
using SurvivalWizard.Spells;
using UnityEngine;

namespace SurvivalWizard.Enemys
{
    public class LeafSlime : Enemy
    {
        [Header("RandomMovingPoint")]
        [SerializeField] private float _wanderRadius;
        [SerializeField] private float _wanderTimer;
        [Header("Attack setting")]
        [SerializeField] private Spell _spellPrefab;
        [SerializeField] private int _numberAttack;

        private RandomMovingEnemyState _randomMovingEnemyState;
        private RangedAttackEnemyState _rangedAttackEnemyState;

        protected override void Start()
        {
            base.Start();
            _randomMovingEnemyState = new RandomMovingEnemyState(this, _wanderRadius, _wanderTimer);
            _rangedAttackEnemyState = new RangedAttackEnemyState(this, _spellPrefab, _delayBetweenAttack, _numberAttack);
            _stateMachine.Initialize(_randomMovingEnemyState);
            Subscribe();
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        private void Subscribe()
        {
            _randomMovingEnemyState.OnStartedMovingEvent += ChangeStateToRangedAttack;
            _rangedAttackEnemyState.OnFinishedAttackEvent += ChangeStateToRandomMoving;
        }
        private void Unsubscribe()
        {
            _randomMovingEnemyState.OnStartedMovingEvent -= ChangeStateToRangedAttack;
            _rangedAttackEnemyState.OnFinishedAttackEvent -= ChangeStateToRandomMoving;
        }

        private void ChangeStateToRandomMoving()
        {
            _stateMachine.ChangeState(_randomMovingEnemyState);
        }

        private void ChangeStateToRangedAttack()
        {
            _stateMachine.ChangeState(_rangedAttackEnemyState);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}