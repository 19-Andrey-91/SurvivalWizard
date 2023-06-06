
using SurvivalWizard.Base;
using System;
using System.Collections.Generic;

namespace SurvivalWizard.PlayerScripts
{
    public class LvlUPManager
    {
        public event Action OnUpgradeSkillEvent;
        public event Action OnAdditionWeaponEvent;

        private List<int> _weaponSelectionLevel;
        private Player _player;

        public LvlUPManager(Player player, List<int> weaponSelectionLevel) 
        {
            _weaponSelectionLevel = weaponSelectionLevel;
            _player = player;
        }

        public void Subscribe()
        {
            _player.PlayerLevel.OnIncreasedLevelEvent += ChooseUpgrade;
        }

        public void Unsubscribe()
        {
            _player.PlayerLevel.OnIncreasedLevelEvent -= ChooseUpgrade;
        }

        private void ChooseUpgrade(int lvl)
        {
            if(_weaponSelectionLevel.Contains(lvl))
            {
                _weaponSelectionLevel.Remove(lvl);
                OnAdditionWeaponEvent?.Invoke();
            }
            else
            {
                OnUpgradeSkillEvent?.Invoke();
            }
        }
    }
}