
using UnityEngine;
using Cinemachine;
using SurvivalWizard.Base;
using SurvivalWizard.Enemys;
using SurvivalWizard.Spells;

namespace SurvivalWizard.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class Character : Entity
    {
        [SerializeField] private Transform _pointSpawnSpell;
        [SerializeField] private Spell _spell;
        public Transform PointSpawnSpell { get => _pointSpawnSpell; }

        private PlayerMovement _playerMovement;
        private Animator _animator;
        private float _timer;
        
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

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _spell.DelayBetweenCast)
            {
                _timer -= _spell.DelayBetweenCast;
                Instantiate(_spell, _pointSpawnSpell.position, _pointSpawnSpell.rotation);
            }
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