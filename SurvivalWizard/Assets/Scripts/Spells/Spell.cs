using SurvivalWizard.Sounds;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] private string _nameSpell;
        [SerializeField] private Sprite _sprite;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _delayBetweenCast;
        [SerializeField] protected float _searchAreaTarget;
        [SerializeField] protected LayerMask _targetLayer;
        protected Collider[] _targetColliders;
        public SoundManager SoundManager { get; set; }

        private ISpellDamaging _currentSpellDamage;

        public string NameSpell { get => _nameSpell; }
        public Sprite Sprite { get => _sprite; }

        public ISpellDamaging CurrentSpellDamage
        {
            get => _currentSpellDamage ??= new SpellDamaging(_damage);
            set => _currentSpellDamage = value ?? throw new UnityException("ISpellDamaging object cannot be null");
        }

        public float DelayBetweenCast { get => _delayBetweenCast; }

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
