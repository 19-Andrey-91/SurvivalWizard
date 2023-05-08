
using SurvivalWizard.Spells;
using SurvivalWizard.Spells.Upgrade;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class ChooseSpellUgradeUI : MonoBehaviour
    {
        public event Action OnUpgradeIsCancelled;

        [SerializeField] private Button _addDamage;
        [SerializeField] private Button _addDurationDamage;

        private Spell _spell;

        public void CurrentSpell(Spell spell)
        {
            Unsubscribe();
            _spell = spell;
            Subscribe();
        }

        private void AddInstantDamage()
        {
            AddBonusDamage(new SpellAdditionalDamage(_spell.CurrentSpellDamage, 20));
        }

        private void AddDurationDamage()
        {
            AddBonusDamage(new SpellDurationDamage(_spell.CurrentSpellDamage, 100, 5, 5));
        }

        private void AddBonusDamage(ISpellDamaging damage)
        {
            if (_spell != null)
            {
                _spell.CurrentSpellDamage = damage;
                OnUpgradeIsCancelled?.Invoke();
            }
            gameObject.SetActive(false);
        }

        private void Subscribe()
        {
            _addDamage.onClick.AddListener(AddInstantDamage);
            _addDurationDamage.onClick.AddListener(AddDurationDamage);
        }
        private void Unsubscribe()
        {
            _addDamage?.onClick.RemoveListener(AddInstantDamage);
            _addDurationDamage?.onClick.RemoveListener(AddDurationDamage);
        }
    }
}
