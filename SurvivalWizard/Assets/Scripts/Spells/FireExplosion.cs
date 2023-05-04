
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    [RequireComponent(typeof(ParticleSystem))]
    public class FireExplosion : AreaSpell
    {
        [SerializeField] float _timeTakeDamaging;

        protected override void Start()
        {
            base.Start();

            Collider target = GetCollider.GetRandomCollider(_targetColliders);
            if (target == null)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = target.transform.position;
                StartCoroutine(DelayExplosion(_timeTakeDamaging));
            }
        }

        private IEnumerator DelayExplosion(float time)
        {
            yield return new WaitForSeconds(time);
            Explosion();
        }
    }
}
