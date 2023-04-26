
using UnityEngine;

namespace SurvivalWizard.Spells
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Spell : MonoBehaviour
    {
        public float LifeTime;
        public float SpellRadius;
        public float DelayBetweenCast;
        public float Damage;
        public float Speed;

        private SphereCollider _myCollider;
        private Rigidbody _myRigidbody;

        private void Awake()
        {
            _myCollider = GetComponent<SphereCollider>();
            _myCollider.isTrigger = true;
            _myCollider.radius = SpellRadius;

            _myRigidbody = GetComponent<Rigidbody>();
            _myRigidbody.isKinematic = true;


            Destroy(gameObject, LifeTime);
        }
    }
}
