
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

        private List<Spell> _currentSpell = new();

        public IEnumerable<Spell> Spells { get => _spells; }
        public IEnumerable<Spell> CurrentSpells { get => _currentSpell; }
        public bool AllSpellsLearned { get => _currentSpell.Count >= _spells.Count; }

        CancellationTokenSource _cancellationTokenSource;

        public void Fire(Transform pointSpawnSpell)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            for (int i = 0; i < _currentSpell.Count; i++)
            {
                if (_currentSpell[i] != null)
                {
                    _ = InstantiateSpellAsync(_currentSpell[i], pointSpawnSpell, _cancellationTokenSource.Token);
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

        public void LearnSpell(Spell spell)
        {
            if (spell != null && !_currentSpell.Contains(spell))
            {
                _currentSpell.Add((Spell)spell.Clone());
            }
        }

        public void AddInstantDamage(Spell spell, float damage)
        {
            spell.CurrentSpellDamage = new SpellAdditionalDamage(spell.CurrentSpellDamage, damage);
        }

        public void AddDurationDamage(Spell spell, float damage, float duration, float partsAmount)
        {
            spell.CurrentSpellDamage = new SpellDurationDamage(spell.CurrentSpellDamage, damage, duration, partsAmount);
        }
    }
}
