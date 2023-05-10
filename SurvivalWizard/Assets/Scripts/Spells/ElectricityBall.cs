
using SurvivalWizard.Enemys;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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
                SearchTarget(_enemiesHitByProjectile);
                SetRotation();
            }
        }
    }
}
