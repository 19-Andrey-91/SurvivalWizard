
using UnityEngine;
using Cinemachine;
using SurvivalWizard.Base;
using SurvivalWizard.Enemies;
using SurvivalWizard.Spells;

namespace SurvivalWizard.PlayerScripts
{
    [RequireComponent(typeof(PlayerMovement), typeof(Animator))]
    public class Player : Entity
    {
        [SerializeField] private Transform _pointSpawnSpell;
        [SerializeField] private SpellBook _spellBook;
        [SerializeField] private int _levelBoostFactor;

        private PlayerLevel _playerLevel;
        private PlayerMovement _playerMovement;
        private Animator _animator;
        private PlayerAnimationController _playerAnimationController;
        private Wallet _wallet;

        public Transform PointSpawnSpell { get => _pointSpawnSpell; }
        public Animator PlayerAnimator { get => _animator ??= GetComponent<Animator>(); }
        public PlayerMovement PlayerMovement { get => _playerMovement ??= GetComponent<PlayerMovement>(); }
        public SpellBook SpellBook { get => _spellBook; }
        public PlayerLevel PlayerLevel { get => _playerLevel; }
        public Wallet Wallet { get => _wallet; }

        private void Awake()
        {
            _playerLevel = new PlayerLevel(_levelBoostFactor);
            _wallet = new Wallet(0);
            _playerAnimationController = new PlayerAnimationController(this, PlayerAnimator);
            SpellBook.Initialize();
        }

        private void Start()
        {
            if (CinemachineCore.Instance.VirtualCameraCount > 0)
            {
                CinemachineCore.Instance.GetVirtualCamera(0).Follow = transform;
            }
            else throw new UnityException("CinemachineVirtualCamera is not found");

            _playerMovement.ChangeSpeed(Speed);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _playerAnimationController.Subscribe();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
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

        public void Fire()
        {
            SpellBook.StopFire();
            SpellBook.Fire(_pointSpawnSpell);
        }
    }
}