

namespace SurvivalWizard.Spells
{
    public class SpellDamaging : ISpellDamaging
    {
        private float _damage;
        public SpellDamaging(float damage)
        {
            _damage = damage;
        }

        public bool ApplyDamage(ICanTakeDamage canTakeDamage)
        {
            return canTakeDamage.TakeDamage(_damage);
        }

        public float GetDamage()
        {
            return _damage;
        }
    }
}
