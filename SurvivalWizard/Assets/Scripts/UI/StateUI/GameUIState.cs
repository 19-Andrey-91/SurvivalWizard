using SurvivalWizard.Base;
using SurvivalWizard.PlayerScripts;
using SurvivalWizard.UI.UIScripts;
using System;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class GameUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private GameUI _gameUI;
        private Player _player;
        private LvlUPManager _lvlUpManager;
        private GameManager _gameManager;
        public GameUIState(LoaderUI loaderUI, GameUI gameUI, GameManager gameManager, Player player)
        {
            _loaderUI = loaderUI;
            _gameUI = gameUI;
            _player = player;
            _lvlUpManager = gameManager.LvlUPManager;
            _gameManager = gameManager;
        }
        public void Enter()
        {
            _gameManager.Pause(false);

            UpdateCountCoins(Bank.Instance.CurrentCoins);

            _gameUI.gameObject.SetActive(true);

            AudioListener.pause = false;

            _player.OnDiedEvent += ChangeUIStateToGameOver;
            _player.OnTakeDamageEvent += UpdateHPBar;
            _lvlUpManager.OnUpgradeSkillEvent += ChangeUIStateToUpgrade;
            _lvlUpManager.OnAdditionWeaponEvent += ChangeUIStateToAddingWeapon;
            _gameUI.PauseButton.onClick.AddListener(ChangeUIStateToPause);
            _player.PlayerLevel.OnIncreasedLevelEvent += UpdateCountLevel;
            _player.PlayerLevel.OnExperienceAddedEvent += UpdateCountExperience;
            Bank.Instance.OnChangeCoinsEvent += UpdateCountCoins;

            UpdateHPBar(_player);
            UpdateCountExperience(0);
        }

        private void UpdateCountExperience(int xp)
        {
            _gameUI.XPBarText.text = $"{xp}/{_player.PlayerLevel.ToLevelUP}";
            _gameUI.XPBarImage.fillAmount = (float)xp / _player.PlayerLevel.ToLevelUP;
        }

        private void UpdateCountLevel(int lvl)
        {
            _gameUI.CountLevelText.text = $"LVL : {lvl}";
        }

        private void UpdateHPBar(Entity player)
        {
            _gameUI.HPBarText.text = $"{player.Hp}/{player.MaxHp}";
            _gameUI.HPBarImage.fillAmount = player.Hp / player.MaxHp;
        }

        private void UpdateCountCoins(int countCoins)
        {
            _gameUI.CountCoinsText.text = $"Coins: {countCoins}";
        }

        public void Exit()
        {
            _lvlUpManager.OnUpgradeSkillEvent -= ChangeUIStateToUpgrade;
            _lvlUpManager.OnAdditionWeaponEvent -= ChangeUIStateToAddingWeapon;
            _player.OnTakeDamageEvent -= UpdateHPBar;
            _player.OnDiedEvent -= ChangeUIStateToGameOver;
            _gameUI.PauseButton.onClick.RemoveListener(ChangeUIStateToPause);
            _player.PlayerLevel.OnIncreasedLevelEvent -= UpdateCountLevel;
            _player.PlayerLevel.OnExperienceAddedEvent -= UpdateCountExperience;
            Bank.Instance.OnChangeCoinsEvent -= UpdateCountCoins;

            AudioListener.pause = true;
            _gameManager.Pause(true);

            _gameUI.gameObject.SetActive(false);
        }

        private void ChangeUIStateToGameOver(Entity player)
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameOverUIState);
        }

        private void ChangeUIStateToPause()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.PauseUIState);
        }

        private void ChangeUIStateToUpgrade()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.UpgradeUIState);
        }

        private void ChangeUIStateToAddingWeapon()
        {
            if (!_player.SpellBook.AllSpellsLearned)
            {
                _loaderUI.StateMachineUI.ChangeState(_loaderUI.AddingWeaponUIState);
            }
        }
    }
}