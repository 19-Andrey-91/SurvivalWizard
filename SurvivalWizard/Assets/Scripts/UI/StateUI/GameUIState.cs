using SurvivalWizard.Base;
using SurvivalWizard.PlayerScripts;
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class GameUIState : IStateUI
    {
        private readonly LoaderUI _loaderUI;
        private readonly GameUI _gameUI;
        private readonly Player _player;
        private readonly LvlUPManager _lvlUpManager;
        private readonly GameManager _gameManager;
        private readonly Bank _bank;
        public GameUIState(LoaderUI loaderUI, GameUI gameUI, GameManager gameManager, Bank bank)
        {
            _loaderUI = loaderUI;
            _gameUI = gameUI;
            _player = gameManager.Player;
            _lvlUpManager = gameManager.LvlUPManager;
            _gameManager = gameManager;
            _bank = bank;
        }
        public void Enter()
        {
            _gameManager.Pause(false);

            UpdateCountCoins(_bank.CurrentCoins);

            _gameUI.gameObject.SetActive(true);

            AudioListener.pause = false;

            _player.OnDiedEvent += ChangeUIStateToGameOver;
            _player.OnTakeDamageEvent += UpdateHPBar;
            _lvlUpManager.OnUpgradeSkillEvent += ChangeUIStateToUpgrade;
            _lvlUpManager.OnAdditionWeaponEvent += ChangeUIStateToAddingWeapon;
            _gameUI.PauseButton.onClick.AddListener(ChangeUIStateToOptions);
            _player.PlayerLevel.OnIncreasedLevelEvent += UpdateCountLevel;
            _player.PlayerLevel.OnExperienceAddedEvent += UpdateCountExperience;
            _bank.OnChangeCoinsEvent += UpdateCountCoins;

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
            _gameUI.PauseButton.onClick.RemoveListener(ChangeUIStateToOptions);
            _player.PlayerLevel.OnIncreasedLevelEvent -= UpdateCountLevel;
            _player.PlayerLevel.OnExperienceAddedEvent -= UpdateCountExperience;
            _bank.OnChangeCoinsEvent -= UpdateCountCoins;

            AudioListener.pause = true;

            _gameUI.gameObject.SetActive(false);
            _bank.SaveCoins();
        }

        private void ChangeUIStateToGameOver(Entity player)
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameOverUIState);
        }

        private void ChangeUIStateToOptions()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.OptionsUIState);
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