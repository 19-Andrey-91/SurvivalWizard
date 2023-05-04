
namespace SurvivalWizard.Spells.Upgrade
{
    public abstract class SpellUpgrade : ISpellDamaging
    {

        protected ISpellDamaging Spell;

        public SpellUpgrade(ISpellDamaging spell)
        {
            Spell = spell;
        }

        public virtual bool ApplyDamage(ICanTakeDamage canTakeDamage)
        {
           return Spell.ApplyDamage(canTakeDamage);
        }

        public virtual float GetDamage()
        {
            return Spell.GetDamage();
        }
    }
}
