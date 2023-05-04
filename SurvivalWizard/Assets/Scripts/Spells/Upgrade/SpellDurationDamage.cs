
namespace SurvivalWizard.Spells.Upgrade
{
    public class SpellDurationDamage : SpellUpgrade
    {
        private float _durationDamage;
        private float _duration;
        private float _partsAmount;
        public SpellDurationDamage(ISpellDamaging spell, float durationDamage, float duration, float partsAmount) : base(spell)
        {
            _duration = duration;
            _partsAmount = partsAmount;
            _durationDamage = durationDamage;
        }

        public override bool ApplyDamage(ICanTakeDamage canTakeDamage)
        {
            if (base.ApplyDamage(canTakeDamage))
            {
                return canTakeDamage.TakeDurationDamage(_durationDamage, _duration, _partsAmount);
            }
            return false;
        }

        public override float GetDamage()
        {
            return base.GetDamage() + _durationDamage;
        }
    }
}
