
using SurvivalWizard.Base;
using UnityEngine;

namespace SurvivalWizard.PlayerScripts
{
    public class PlayerAnimationController
    {
        private Animator _animator;
        private Player _player;

        public PlayerAnimationController(Player player, Animator animator) 
        {
            _player = player;
            _animator = animator;
        }

        private void GetHit(Entity entity)
        {
            _animator.SetTrigger("GetHit");
        }

        private void Run(bool run)
        {
            _animator.SetBool("Run", run);
        }

        public void Subscribe()
        {
            _player.PlayerMovement.OnPlayerMoveEvent += Run;
            _player.OnTakeDamageEvent += GetHit;
        }

        public void Unsubscribe()
        {
            _player.PlayerMovement.OnPlayerMoveEvent -= Run;
            _player.OnTakeDamageEvent -= GetHit;
        }
    }
}
