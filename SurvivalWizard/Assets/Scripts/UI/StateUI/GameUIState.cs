using SurvivalWizard.Base;
using SurvivalWizard.Enemys;
using SurvivalWizard.PlayerScripts;
using SurvivalWizard.Sounds;
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class GameUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private GameUI _gameUI;
        private Player _player;
        private EnemySpawner _enemySpawner;
        public GameUIState(LoaderUI loaderUI, GameUI gameUI)
        {
            _loaderUI = loaderUI;
            _gameUI = gameUI;
        }
        public void Enter()
        {
            Time.timeScale = 1f;

            _player = GameManager.Instance.Player;
            _enemySpawner = GameManager.Instance.EnemySpawner;

            _gameUI.gameObject.SetActive(true);

            AudioListener.pause = false;

            _player.OnDiedEvent += ChangeUIStateToGameOver;
            _player.OnTakeDamageEvent += UpdateHPBar;
            _enemySpawner.OnUpdatedCountKillsEvent += UpdateCountKills;
            _enemySpawner.OnUpgradeSkill += ChangeUIStateToUpgrade;
            _gameUI.PauseButton.onClick.AddListener(ChangeUIStateToPause);

            UpdateHPBar(_player);
        }

        private void UpdateHPBar(Entity player)
        {
            _gameUI.HPBarText.text = $"{player.Hp}/{player.MaxHp}";
            _gameUI.HPBarImage.fillAmount = player.Hp / player.MaxHp;
        }

        private void UpdateCountKills(int countKills)
        {
            _gameUI.CountKillsText.text = $"Kills: {countKills}";
        }

        public void Exit()
        {
            _enemySpawner.OnUpgradeSkill -= ChangeUIStateToUpgrade;
            _enemySpawner.OnUpdatedCountKillsEvent -= UpdateCountKills;
            _player.OnTakeDamageEvent -= UpdateHPBar;
            _player.OnDiedEvent -= ChangeUIStateToGameOver;
            _gameUI.PauseButton.onClick.RemoveListener(ChangeUIStateToPause);

            AudioListener.pause = true;

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
    }
}