
using Cysharp.Threading.Tasks;
using SurvivalWizard.Spells.Upgrade;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    [Serializable]
    public class SpellBook
    {
        [SerializeField] private List<Spell> _spells;

        public List<Spell> Spells { get => _spells; }

        CancellationTokenSource _cancellationTokenSource;

        public void SpellUpgrade()
        {
            foreach (Spell spell in Spells)
            {
                spell.CurrentSpellDamage = new SpellDurationDamage(spell.CurrentSpellDamage, 3000, 10, 10);
                spell.CurrentSpellDamage = new SpellDurationDamage(spell.CurrentSpellDamage, 3000, 10, 10);
            }
        }

        public void Fire(Transform pointSpawnSpell)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            for (int i = 0; i < _spells.Count; i++)
            {
                if (_spells[i] != null)
                {
                    _ = InstantiateSpellAsync(_spells[i], pointSpawnSpell, _cancellationTokenSource.Token);
                }
            }
        }

        private async UniTaskVoid InstantiateSpellAsync(Spell spell, Transform pointSpawnSpell, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Spell newSpell = GameObject.Instantiate(spell, pointSpawnSpell.position, pointSpawnSpell.rotation);
                newSpell.CurrentSpellDamage = spell.CurrentSpellDamage;
                await UniTask.Delay(TimeSpan.FromSeconds(spell.DelayBetweenCast), cancellationToken: token).SuppressCancellationThrow();
            }
        }

        public void StopFire()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
