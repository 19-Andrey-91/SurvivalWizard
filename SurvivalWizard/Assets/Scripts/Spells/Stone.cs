
using SurvivalWizard.Enemys;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public class Stone : FlyingSpell
    {
        public const string Name = "Stone";
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                CurrentSpellDamage.ApplyDamage(enemy);
                Destroy(gameObject);
            }
        }
    }
}
