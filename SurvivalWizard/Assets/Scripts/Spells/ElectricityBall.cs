
using SurvivalWizard.Enemys;
using System.Collections.Generic;
using UnityEngine;

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
                _soundManager.EffectsAudioSource.PlayOneShot(_soundManager.Sounds.GetValueDictionary(_nameSoundRicochet));
                _enemiesHitByProjectile.Add(other);
                CurrentSpellDamage.ApplyDamage(enemy);
                SearchTargets(_enemiesHitByProjectile);
                FindNearestColliderAndSetRotation();
            }
        }
    }
}
