
using SurvivalWizard.Enemys;
using SurvivalWizard.PlayerScripts;
using System;
using UnityEngine;

namespace SurvivalWizard.EnemyState
{
    public class ExplosionEnemyState : IStateEnemy
    {
        public event Action OnExplodedEvent;

        private Enemy _enemy;
        private Player _target;
        private Animator _animator;
        private float _damage;
        private float _explosionRadius;
        public ExplosionEnemyState(Enemy enemy, Player target, float damage, float explosionRadius)
        {
            _animator = enemy.Animator;
            _enemy = enemy;
            _target = target;
            _damage = damage;
            _explosionRadius = explosionRadius;
            
        }
        public void Enter()
        {
            Explosion();
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }

        private void Explosion()
        {
            var distance = Vector3.Distance(_enemy.transform.position, _target.transform.position);
            if (distance <= _explosionRadius) 
            {
                _target.TakeDamage(_damage);
            }
            
            OnExplodedEvent?.Invoke();
            _enemy.TakeDamage(_damage);
        }
    }
}
