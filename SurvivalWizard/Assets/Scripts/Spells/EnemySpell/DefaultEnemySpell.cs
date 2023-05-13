
using SurvivalWizard.Base;
using SurvivalWizard.PlayerScripts;
using UnityEngine;

namespace SurvivalWizard.Spells.EnemySpell
{
    public class DefaultEnemySpell : FlyingSpell
    {
        private void Start()
        {
            SetRotation(GameManager.Instance.Player.PlayerMovement.CharacterController);
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
