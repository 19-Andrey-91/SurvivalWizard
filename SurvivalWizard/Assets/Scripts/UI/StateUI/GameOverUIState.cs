
using SurvivalWizard.Base;
using SurvivalWizard.UI.UIScripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SurvivalWizard.UI.StateUI
{
    public class GameOverUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private GameOverUI _gameOverUI;
        public GameOverUIState(LoaderUI loaderUI, GameOverUI gameOverUI)
        {
            _loaderUI = loaderUI;
            _gameOverUI = gameOverUI;
        }

        public void Enter()
        {
            _gameOverUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
            _gameOverUI.RestartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
        }

        public void Exit()
        {
            _gameOverUI.RestartButton.onClick.RemoveListener(RestartGame);
            _gameOverUI.gameObject.SetActive(false);
        }
    }
}
