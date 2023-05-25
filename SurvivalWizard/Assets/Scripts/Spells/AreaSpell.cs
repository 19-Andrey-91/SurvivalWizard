
using SurvivalWizard.Enemies;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public abstract class AreaSpell : Spell
    {
        [SerializeField] protected float _damageRadius;
        protected void Explosion()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _damageRadius, _targetLayer);

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    CurrentSpellDamage.ApplyDamage(enemy);
                }
            }
        }
    }
}
