
using UnityEngine;
using Cinemachine;
using SurvivalWizard.Base;
using SurvivalWizard.Enemies;
using SurvivalWizard.Spells;
using Zenject;
using SurvivalWizard.Sounds;

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
        private SoundManager _soundManager;

        public Transform PointSpawnSpell { get => _pointSpawnSpell; }
        public Animator PlayerAnimator { get => _animator ??= GetComponent<Animator>(); }
        public PlayerMovement PlayerMovement { get => _playerMovement ??= GetComponent<PlayerMovement>(); }
        public SpellBook SpellBook { get => _spellBook; }
        public PlayerLevel PlayerLevel { get => _playerLevel ??= new PlayerLevel(_levelBoostFactor); }

        [Inject]
        private void Construct(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

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

            SpellBook.Initialize(_soundManager);
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

        public void UpdateStats()
        {
            Hp = MaxHp;
            PlayerMovement.ChangeSpeed(Speed);
        }
    }
}