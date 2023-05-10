﻿
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    [RequireComponent(typeof(ParticleSystem))]
    public class FireExplosion : AreaSpell
    {
        [SerializeField] float _timeTakeDamaging;

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
            SearchTarget();

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
            }
        }
    }
}
