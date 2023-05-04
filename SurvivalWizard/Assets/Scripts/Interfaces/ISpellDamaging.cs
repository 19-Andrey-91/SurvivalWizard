
namespace SurvivalWizard
{
    public interface ISpellDamaging
    {
        public float GetDamage();
        public bool ApplyDamage(ICanTakeDamage canTakeDamage);
    }
}
