
using UnityEngine;
using Cinemachine;
using SurvivalWizard.Base;
using SurvivalWizard.Enemys;
using SurvivalWizard.Spells;

namespace SurvivalWizard.PlayerScripts
{
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : Entity
    {
        [SerializeField] private Transform _pointSpawnSpell;
        [SerializeField] private SpellBook _spellBook;

        private PlayerMovement _playerMovement;
        private Animator _animator;

        public Transform PointSpawnSpell { get => _pointSpawnSpell; }
        public Animator PlayerAnimator { get { return _animator ??= GetComponentInChildren<Animator>(); } }

        public SpellBook SpellBook { get => _spellBook ??= new SpellBook(); }

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
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

        private void OnDisable()
        {
            SpellBook.StopFire();
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