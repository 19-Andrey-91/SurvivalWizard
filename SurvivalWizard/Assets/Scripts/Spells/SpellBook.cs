
using Cysharp.Threading.Tasks;
using SurvivalWizard.Sounds;
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

        private List<Spell> _currentSpells = new();
        private SpellUpgradeData _upgradeData;
        private SoundManager _soundManager;

        public IEnumerable<Spell> Spells { get => _spells; }
        public IEnumerable<Spell> CurrentSpells { get => _currentSpells; }
        public bool AllSpellsLearned { get => _currentSpells.Count >= _spells.Count; }

        CancellationTokenSource _cancellationTokenSource;

        public void Initialize(SoundManager soundManager)
        {
            _upgradeData = new SpellUpgradeData(_spells);
            _soundManager = soundManager;
        }

        public void Fire(Transform pointSpawnSpell)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            for (int i = 0; i < _currentSpells.Count; i++)
            {
                if (_currentSpells[i] != null)
                {
                    _ = InstantiateSpellAsync(_currentSpells[i], pointSpawnSpell, _cancellationTokenSource.Token);
                }
            }
        }

        private async UniTaskVoid InstantiateSpellAsync(Spell spell, Transform pointSpawnSpell, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Spell newSpell = GameObject.Instantiate(spell, pointSpawnSpell.position, pointSpawnSpell.rotation, null);
                newSpell.SoundManager = _soundManager;
                newSpell.CurrentSpellDamage = _upgradeData.GetSpellDamage(newSpell.NameSpell);
                await UniTask.Delay(TimeSpan.FromSeconds(spell.DelayBetweenCast), cancellationToken: token).SuppressCancellationThrow();
            }
        }

        public void StopFire()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void LearnSpell(Spell spell)
        {
            if (spell != null && !_currentSpells.Contains(spell))
            {
                _currentSpells.Add(spell);
            }
        }

        public void AddInstantDamage(Spell spell, float damage)
        {
            _upgradeData.AddInstantDamage(spell.NameSpell, damage);
        }

        public void AddDurationDamage(Spell spell, float damage, float duration, float partsAmount)
        {
            _upgradeData.AddDurationDamage(spell.NameSpell, damage, duration, partsAmount);
        }
    }
}
