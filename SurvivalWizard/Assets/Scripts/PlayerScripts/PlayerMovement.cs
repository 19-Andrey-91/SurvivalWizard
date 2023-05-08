
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace SurvivalWizard.PlayerScripts
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public event Action<bool> OnPlayerMoveEvent;

        [SerializeField] private float _rotationSpeed = 10f;

        private float _speed;
        private PlayerControls _controls;
        private Vector3 _moveDirection = Vector3.zero;
        private CharacterController _controller;

        public CharacterController CharacterController { get { return _controller ??= GetComponent<CharacterController>(); } }

        private void Awake()
        {
            _controls = new PlayerControls();
        }

        private void Update()
        {
            if (_moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_moveDirection), Time.deltaTime * _rotationSpeed);
            }
            CharacterController.SimpleMove(_moveDirection);
        }

        private void OnEnable()
        {
            _controls.Enable();
            _controls.Controls.Movement.performed += Move;
            _controls.Controls.Movement.canceled += StopMove;
        }

        private void OnDisable()
        {
            _controls.Controls.Movement.performed -= Move;
            _controls.Controls.Movement.canceled -= StopMove;
            _controls.Enable();
        }

        private void Move(InputAction.CallbackContext obj)
        {
            OnPlayerMoveEvent?.Invoke(true);
            Vector2 inputDirection = obj.ReadValue<Vector2>();
            _moveDirection.x = inputDirection.x * _speed;
            _moveDirection.z = inputDirection.y * _speed;
        }

        private void StopMove(InputAction.CallbackContext obj)
        {
            _moveDirection = Vector3.zero;
            OnPlayerMoveEvent?.Invoke(false);
        }

        public void ChangeSpeed(float speed)
        {
            _speed = speed;
        }
    }
}