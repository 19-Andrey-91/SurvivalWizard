
using SurvivalWizard.Base;
using SurvivalWizard.UI.UIScripts;
using UnityEngine.SceneManagement;

namespace SurvivalWizard.UI.StateUI
{
    public class GameOverUIState : IStateUI
    {
        private readonly LoaderUI _loaderUI;
        private readonly GameOverUI _gameOverUI;
        private readonly GameManager _gameManager;

        public GameOverUIState(LoaderUI loaderUI, GameOverUI gameOverUI, GameManager gameManager)
        {
            _loaderUI = loaderUI;
            _gameOverUI = gameOverUI;
            _gameManager = gameManager;
        }

        public void Enter()
        {
            _gameOverUI.gameObject.SetActive(true);
            _gameManager.Pause(true);
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
