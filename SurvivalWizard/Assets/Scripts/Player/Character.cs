
using UnityEngine;
using Cinemachine;
using SurvivalWizard.Base;
using SurvivalWizard.Enemys;

namespace SurvivalWizard.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class Character : Entity
    {
        [SerializeField] private Transform _pointSpawnWeapon;
        public Transform PointSpawnWeapon { get => _pointSpawnWeapon; }

        private PlayerMovement _playerMovement;
        private Animator _animator;
        
        public Animator CharacterAnimator { get { return _animator ??= GetComponentInChildren<Animator>(); } }

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
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
}