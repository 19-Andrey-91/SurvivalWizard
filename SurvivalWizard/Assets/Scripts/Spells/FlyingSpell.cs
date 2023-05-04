﻿using UnityEngine;

namespace SurvivalWizard.Spells
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    public abstract class FlyingSpell : Spell
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;

        private SphereCollider _myCollider;
        private Rigidbody _myRigidbody;

        public float Speed { get => _speed; }

        private void Awake()
        {
            _myCollider = GetComponent<SphereCollider>();
            _myCollider.isTrigger = true;

            _myRigidbody = GetComponent<Rigidbody>();
            _myRigidbody.isKinematic = true;

            Destroy(gameObject, _lifeTime);
        }

        protected virtual void Update()
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }
}
