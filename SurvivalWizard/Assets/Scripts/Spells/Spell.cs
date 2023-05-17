using SurvivalWizard.Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] private string _nameSpell;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _delayBetweenCast;
        [SerializeField] protected float _searchAreaTarget;
        [SerializeField] protected LayerMask _targetLayer;
        protected Collider[] _targetColliders;
        protected SoundManager _soundManager;

        private ISpellDamaging _currentSpellDamage;

        public string NameSpell { get => _nameSpell; }

        public ISpellDamaging CurrentSpellDamage
        {
            get => _currentSpellDamage ??= new SpellDamaging(_damage);
            set
            {
                _currentSpellDamage = value ?? throw new UnityException("ISpellDamaging object cannot be null");
            }
        }

        public float DelayBetweenCast { get => _delayBetweenCast; }

        protected virtual void Awake()
        {
            _soundManager = SoundManager.Instance;
        }

        protected void SearchTargets(List<Collider> exceptTargets = null)
        {
            _targetColliders = Physics.OverlapSphere(transform.position, _searchAreaTarget, _targetLayer); ;
            if (exceptTargets != null)
            {
                _targetColliders = _targetColliders.Except(exceptTargets).ToArray();
            }
        }
    }
}
