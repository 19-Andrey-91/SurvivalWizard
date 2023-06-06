
using SurvivalWizard.Enemies;
using SurvivalWizard.Sounds;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SurvivalWizard.Spells
{
    public class ElectricityBall : FlyingSpell
    {
        [Header("Sound")]
        [SerializeField] private string _nameSoundRicochet;

        private List<Collider> _enemiesHitByProjectile = new List<Collider>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                SoundManager.PlaySound(_nameSoundRicochet, transform.position);
                _enemiesHitByProjectile.Add(other);
                CurrentSpellDamage.ApplyDamage(enemy);
                SearchTargets(_enemiesHitByProjectile);
                FindNearestColliderAndSetRotation();
            }
        }
    }
}
