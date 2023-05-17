
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    [RequireComponent(typeof(ParticleSystem))]
    public class FireExplosion : AreaSpell
    {
        [SerializeField] private float _timeTakeDamaging;
        [Header("Sound")]
        [SerializeField] private string _nameSoundExplosion;
        
        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
        }

        private void Start()
        {
            SearchTargets();

            Collider target = GetCollider.GetRandomCollider(_targetColliders);
            if (target == null)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = target.transform.position;
                _ = DelayExplosion(_timeTakeDamaging, _cancellationTokenSource.Token);
            }
        }

        private async UniTaskVoid DelayExplosion(float time, CancellationToken token)
        {
            bool isCancelled = await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token).SuppressCancellationThrow();
            if (!isCancelled)
            {
                Explosion();
                _soundManager.EffectsAudioSource.PlayOneShot(_soundManager.Sounds.GetValueDictionary(_nameSoundExplosion));
            }
        }
    }
}
