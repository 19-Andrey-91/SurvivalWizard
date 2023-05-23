
using System.Collections.Generic;

namespace SurvivalWizard.Spells.Upgrade
{
    public class SpellUpgradeData
    {
        private Dictionary<string, ISpellDamaging> _spellsDamage = new Dictionary<string, ISpellDamaging>();

        public SpellUpgradeData(IEnumerable<Spell> spells) 
        {
            foreach(Spell spell in spells)
            {
                _spellsDamage.Add(spell.NameSpell, spell.CurrentSpellDamage);
            }
        }

        public ISpellDamaging GetSpellDamage(string name)
        {
            if (_spellsDamage.ContainsKey(name))
            {
                return _spellsDamage[name];
            }
            return null;
        }

        public void AddInstantDamage(string spellName, float damage)
        {
            _spellsDamage[spellName] = new SpellAdditionalDamage(_spellsDamage[spellName], damage);
        }

        public void AddDurationDamage(string spellName, float damage, float duration, float partsAmount)
        {
            _spellsDamage[spellName] = new SpellDurationDamage(_spellsDamage[spellName], damage, duration, partsAmount);
        }
    }
}
