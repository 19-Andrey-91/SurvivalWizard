
using SurvivalWizard.Enemys;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public class Fireball : FlyingSpell
    {
        private  void Start()
        {
            SearchTargets();
            FindNearestColliderAndSetRotation();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                CurrentSpellDamage.ApplyDamage(enemy);
            }
        }
    }
}
