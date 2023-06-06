using SurvivalWizard.PlayerScripts;
using UnityEngine;

namespace SurvivalWizard.Spells.EnemySpell
{
    public class DefaultEnemySpell : FlyingSpell
    {
        private Player _target;

        public void Initialize(Player target)
        {
            _target = target;
        }

        private void Start()
        {
            SetRotation(_target.PlayerMovement.CharacterController);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                CurrentSpellDamage.ApplyDamage(player);
            }
        }
    }
}
