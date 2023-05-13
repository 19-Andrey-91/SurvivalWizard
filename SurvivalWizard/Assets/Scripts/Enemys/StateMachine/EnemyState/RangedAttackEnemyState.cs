
using SurvivalWizard.Enemys;
using SurvivalWizard.Spells;
using System;
using UnityEngine;

namespace SurvivalWizard.EnemyState
{
    public class RangedAttackEnemyState : IStateEnemy
    {
        public event Action OnFinishedAttackEvent;

        private readonly int _numberAttact;

        private Spell _spellPrefab;
        private float _delayBetweenAttack;
        private Transform _transform;
        private Animator _animator;
        private Enemy _enemy;
        private float _timer;
        private int _countAttack;

        public RangedAttackEnemyState(Enemy enemy, Spell spellPrefab, float delayBetweenAttack, int numberAttack)
        {
            _enemy = enemy;
            _spellPrefab = spellPrefab;
            _animator = enemy.Animator;
            _delayBetweenAttack = delayBetweenAttack;
            _transform = enemy.transform;
            _numberAttact = numberAttack;
        }

        public void Enter()
        {
            _countAttack = _numberAttact;
        }

        public void Exit()
        {

        }

        public void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _delayBetweenAttack)
            {
                _animator.SetTrigger("Attack");
                GameObject.Instantiate(_spellPrefab, _transform.position, Quaternion.identity);
                _timer -= _delayBetweenAttack;
                _countAttack--;
                if (_countAttack <= 0)
                {
                    OnFinishedAttackEvent();
                }
            }
        }
    }
}
