using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SurvivalWizard.Base
{
    [RequireComponent(typeof(Collider))]
    public abstract class Entity : MonoBehaviour, ICanTakeDamage
    {
        public event Action<Entity> OnDiedEvent;
        public event Action<Entity> OnTakeDamageEvent;

        [SerializeField] protected float _hp;
        [SerializeField] protected float _maxHp;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _attackSpeed;

        private CancellationTokenSource _cancellationTokenSource;

        public float Hp
        {
            get => _hp;
            set
            {
                if (value > _maxHp)
                {
                    _hp = _maxHp;
                    return;
                }
                _hp = value;
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
            Debug.Log($"{this}, {damage}");

            if (_hp <= 0)
            {
                _cancellationTokenSource.Cancel();

                OnDiedEvent?.Invoke(this);

                return false;
            }
            return true;
        }

        private float CheckValue(float value)
        {
            if (value < 0)
            {
                value = 0;
            }
            return value;
        }

        private void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTaskVoid ApplyDurationDamageAsync(float durationDamage, float duration, float partsAmount, CancellationToken token)
        {
            float damage = Mathf.Ceil(durationDamage / partsAmount);
            float partDuration = duration / partsAmount;

            for (int i = 0; i < partsAmount; i++)
            {
                try
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(partDuration), cancellationToken: token);
                }
                catch
                {
                    break;
                }

                TakeDamage(damage);
            }
        }

        public bool TakeDurationDamage(float durationDamage, float duration, float partsAmount)
        {
            _ = ApplyDurationDamageAsync(durationDamage, duration, partsAmount, _cancellationTokenSource.Token);

            return !_cancellationTokenSource.Token.IsCancellationRequested;
        }
    }
}