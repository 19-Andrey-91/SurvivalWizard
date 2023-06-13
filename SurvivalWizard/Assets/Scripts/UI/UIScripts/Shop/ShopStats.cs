
using System.Collections.Generic;

namespace SurvivalWizard
{
    [System.Serializable]
    public class ShopStats
    {
        public string StatName;
        public TypeStat TypeStat;
        public bool IsUnlocked;
        public int UnlockedLevel;
        public List<StatInfo> StatInfo;
    }
}
