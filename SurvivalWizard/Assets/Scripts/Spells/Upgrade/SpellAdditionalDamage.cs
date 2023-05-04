
namespace SurvivalWizard.Spells.Upgrade
{
    public class SpellAdditionalDamage : SpellUpgrade
    {
        private float _additionalDamage;
        public SpellAdditionalDamage(ISpellDamaging spell, float additionalDamage) : base(spell)
        {
            _additionalDamage = additionalDamage;
        }

        public override bool ApplyDamage(ICanTakeDamage canTakeDamage)
        {
            if (base.ApplyDamage(canTakeDamage))
            {
                return canTakeDamage.TakeDamage(_additionalDamage);
            }
            return false;
        }

        public override float GetDamage()
        {
            return base.GetDamage() + _additionalDamage;
        }
    }
}
