
using UnityEngine;
using Cinemachine;
using SurvivalWizard.Base;
using SurvivalWizard.Enemys;
using SurvivalWizard.Spells;

namespace SurvivalWizard.PlayerScripts
{
    [RequireComponent(typeof(PlayerMovement), typeof(Animator))]
    public class Player : Entity
    {
        [SerializeField] private Transform _pointSpawnSpell;
        [SerializeField] private SpellBook _spellBook;

        private PlayerMovement _playerMovement;
        private Animator _animator;
        private PlayerAnimationController _playerAnimationController;

        public Transform PointSpawnSpell { get => _pointSpawnSpell; }
        public Animator PlayerAnimator { get => _animator ??= GetComponent<Animator>(); }
        public PlayerMovement PlayerMovement { get => _playerMovement ??= GetComponent<PlayerMovement>(); }
        public SpellBook SpellBook { get => _spellBook ??= new SpellBook(); }

        private void Awake()
        {
            _playerAnimationController = new PlayerAnimationController(this, PlayerAnimator);
        }

        private void Start()
        {
            if (CinemachineCore.Instance.VirtualCameraCount > 0)
            {
                CinemachineCore.Instance.GetVirtualCamera(0).Follow = transform;
            }
            else throw new UnityException("CinemachineVirtualCamera is not found");

            _playerMovement.ChangeSpeed(Speed);

            SpellBook.Fire(_pointSpawnSpell);
        }

        private void OnEnable()
        {
            _playerAnimationController.Subscribe();
        }

        private void OnDisable()
        {
            SpellBook.StopFire();
            _playerAnimationController.Unsubscribe();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
}