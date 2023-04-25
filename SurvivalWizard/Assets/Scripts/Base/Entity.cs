using System;
using UnityEngine;

namespace SurvivalWizard.Base
{
    [RequireComponent(typeof(Collider))]
    public abstract class Entity : MonoBehaviour
    {
        public event Action<Entity> OnDiedEvent;
        public event Action<Entity> OnTakeDamageEvent;

        [SerializeField] protected float _hp;
        [SerializeField] protected float _maxHp;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _attackSpeed;

        public float Hp
        {
            get => _hp;
            set
            {
                if (value > _maxHp)
                {
                    _hp = _maxHp;
                }
            }
        }
        public float MaxHp { get => _maxHp; set => _maxHp = CheckValue(value); }
        public float Damage { get => _damage; set => CheckValue(value); }
        public float Speed { get => _speed; set => _speed = CheckValue(value); }
        public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = CheckValue(value); }

        public bool TakeDamage(float damage)
        {
            _hp -= damage;
            OnTakeDamageEvent?.Invoke(this);
            if (_hp <= 0)
            {
                OnDiedEvent?.Invoke(this);
                return true;
            }
            return false;
        }

        private float CheckValue(float value)
        {
            if (value < 0)
            {
                value = 0;
            }
            return value;
        }
    }
}