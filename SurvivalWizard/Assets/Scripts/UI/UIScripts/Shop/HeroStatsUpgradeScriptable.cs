
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalWizard
{
    [CreateAssetMenu(fileName = "ShopStats", menuName = "Scriptable/Create ShopStats")]
    public class HeroStatsUpgradeScriptable : ScriptableObject
    {
        [SerializeField] private List<ShopStats> _shopStats;

        public IEnumerable<ShopStats> ShopStats { get => _shopStats; set => _shopStats = value as List<ShopStats> ?? _shopStats; }
    }
}
