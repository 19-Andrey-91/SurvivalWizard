
using SurvivalWizard.Enemies;
using SurvivalWizard.Sounds;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public class Fireball : FlyingSpell
    {
        [Header("Sound")]
        [SerializeField] private string _nameFireballSound;
        private void Start()
        {
            SearchTargets();
            FindNearestColliderAndSetRotation();
            SoundManager.Instance.PlaySound(_nameFireballSound, transform.position);
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
