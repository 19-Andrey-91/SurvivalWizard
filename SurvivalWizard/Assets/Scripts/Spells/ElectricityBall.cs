
using SurvivalWizard.Enemys;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public class ElectricityBall : FlyingSpell
    {
        private List<Collider> _enemiesHitByProjectile = new List<Collider>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                _enemiesHitByProjectile.Add(other);
                CurrentSpellDamage.ApplyDamage(enemy);
                SearchTargets(_enemiesHitByProjectile);
                FindNearestColliderAndSetRotation();
            }
        }
    }
}
