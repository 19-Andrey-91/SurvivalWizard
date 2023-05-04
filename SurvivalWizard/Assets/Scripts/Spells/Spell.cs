﻿
using Unity.VisualScripting;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] protected float _damage;
        [SerializeField] protected float _delayBetweenCast;
        [SerializeField] protected float _searchAreaTarget;
        [SerializeField] protected LayerMask _targetLayer;
        protected Collider[] _targetColliders;

        private ISpellDamaging _currentSpellDamage;
        public ISpellDamaging CurrentSpellDamage
        {
            get => _currentSpellDamage ??= new SpellDamaging(_damage);
            set
            {
                _currentSpellDamage = value ?? throw new UnityException("ISpellDamaging object cannot be null");
            }
        }

        public float DelayBetweenCast { get => _delayBetweenCast; }

        protected virtual void Start()
        {
            _targetColliders = Physics.OverlapSphere(transform.position, _searchAreaTarget, _targetLayer);
        }
    }
}
