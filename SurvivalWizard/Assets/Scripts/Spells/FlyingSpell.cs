using System.Collections.Generic;
using UnityEngine;

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

        protected override void Awake()
        {
            base.Awake();
            _myCollider = GetComponent<SphereCollider>();
            _myCollider.isTrigger = true;

            _myRigidbody = GetComponent<Rigidbody>();
            _myRigidbody.isKinematic = true;

            Destroy(gameObject, _lifeTime);
        }

        protected void FindNearestColliderAndSetRotation()
        {
            Collider nearestTarget = GetCollider.GetNearestCollider(transform, _targetColliders);

            if (nearestTarget == null)
            {
                return;
            }
            SetRotation(nearestTarget);
        }

        protected void SetRotation(Collider target)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        protected virtual void Update()
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }
}
