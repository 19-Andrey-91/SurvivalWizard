
using SurvivalWizard.Base;
using SurvivalWizard.UI.UIScripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.StateUI
{
    public class ShopUIState : IStateUI
    {
        private const string _nameHeroStatsUpgradeFile = "ShopStats";
        private const string _nameLoadFile = "ShopUpgrades";

        private readonly LoaderUI _loaderUI;
        private readonly ShopUI _shopUI;
        private readonly Bank _bank;
        private readonly GameManager _gameManager;
        private HeroStatsUpgradeScriptable _heroStatsUpgradeScriptable;

        public ShopUIState(LoaderUI loaderUI, ShopUI shopUI, GameManager gameManager, Bank bank)
        {
            _loaderUI = loaderUI;
            _shopUI = shopUI;
            _bank = bank;
            _gameManager = gameManager;
            _heroStatsUpgradeScriptable = Resources.Load<HeroStatsUpgradeScriptable>(_nameHeroStatsUpgradeFile);
            bank.StorageService.Load<IEnumerable<ShopStats>>(_nameLoadFile, LoadShopStats);
        }

        private void LoadShopStats(IEnumerable<ShopStats> shopStats)
        {
            if (shopStats != null)
            {
                _heroStatsUpgradeScriptable.ShopStats = shopStats;
            }
            CreateStatUpgradeButtons();
        }

        public void Enter()
        {
            UpdateCountCoins(_bank.CurrentCoins);

            _bank.OnChangeCoinsEvent += UpdateCountCoins;
            _shopUI.BackButton.onClick.AddListener(ChangeStateToStartMenuUI);
            _shopUI.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _bank.OnChangeCoinsEvent -= UpdateCountCoins;
            _shopUI.BackButton.onClick.RemoveListener(ChangeStateToStartMenuUI);
            _shopUI.gameObject.SetActive(false);
            _bank.StorageService.Save(_nameLoadFile, _heroStatsUpgradeScriptable.ShopStats);
        }

        private void UpdateCountCoins(int currentCoins)
        {
            _shopUI.CurrentCoins.text = currentCoins.ToString();
        }

        private void CreateStatUpgradeButtons()
        {
            foreach (ShopStats stat in _heroStatsUpgradeScriptable.ShopStats)
            {
                ButtonUpgrade buttonUpgrade = GameObject.Instantiate(_shopUI.PrefabUpgradeButton, _shopUI.ContainerUpgradeButtons);
                ButtonSubscription(buttonUpgrade, stat);
                UpdateButtonText(stat, buttonUpgrade);
            }
        }

        private void UpdateButtonText(ShopStats stat, ButtonUpgrade buttonUpgrade)
        {
            buttonUpgrade.NameUpgrade.text = stat.StatName;
            buttonUpgrade.LvlUpgrade.text = $"{stat.UnlockedLevel} / {stat.StatInfo.Count}";
            if (stat.UnlockedLevel < stat.StatInfo.Count)
            {
                buttonUpgrade.PriceUpgrade.text = stat.StatInfo[stat.UnlockedLevel].UnlockCost.ToString();
            }
            else
            {
                stat.IsUnlocked = true;
                buttonUpgrade.PriceUpgrade.text = "Full";
            }
        }

        private void ChangeStateToStartMenuUI()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.StartMenuUIState);
        }

        private void ButtonSubscription(ButtonUpgrade buttonUpgrade, ShopStats stats)
        {
            buttonUpgrade.GetComponent<Button>().onClick.AddListener(UpStat);

            void UpStat()
            {
                if (StatUpgrade(stats))
                {
                    UpdateButtonText(stats, buttonUpgrade);
                }
            }
        }

        private bool StatUpgrade(ShopStats stats)
        {
            if (stats.IsUnlocked)
            {
                return false;
            }
            if (!_bank.TryPayCoins(stats.StatInfo[stats.UnlockedLevel].UnlockCost))
            {
                return false;
            }
            if (stats.UnlockedLevel >= stats.StatInfo.Count)
            {
                return false;
            }

            switch (stats.TypeStat)
            {
                case TypeStat.MaxHp:
                    {
                        _gameManager.Player.MaxHp *= stats.StatInfo[stats.UnlockedLevel].Multiplier;
                        _gameManager.Player.UpdateStats();
                        break;
                    }
                case TypeStat.MoveSpeed:
                    {
                        _gameManager.Player.Speed *= stats.StatInfo[stats.UnlockedLevel].Multiplier;
                        _gameManager.Player.UpdateStats();
                        break;
                    }

            }
            stats.UnlockedLevel++;
            return true;
        }
    }
}
