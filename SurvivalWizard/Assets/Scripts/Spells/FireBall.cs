
using SurvivalWizard.Enemys;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public class Fireball : FlyingSpell
    {
        protected override void Start()
        {
            base.Start();
            SetRotation();
        }

        private void SetRotation()
        {
            Collider nearestTarget = GetCollider.GetNearestCollider(transform, _targetColliders);

            if (nearestTarget == null)
            {
                return;
            }

            Vector3 direction = nearestTarget.transform.position - transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                CurrentSpellDamage.ApplyDamage(enemy);
            }
        }
    }
}
