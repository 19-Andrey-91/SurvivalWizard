
using SurvivalWizard.Enemys;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public class Fireball : FlyingSpell
    {
        [Header("Sound")]
        [SerializeField] private string _nameFireballSound;
        private  void Start()
        {
            SearchTargets();
            FindNearestColliderAndSetRotation();
            _soundManager.EffectsAudioSource.PlayOneShot(_soundManager.Sounds.GetValueDictionary(_nameFireballSound));
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
