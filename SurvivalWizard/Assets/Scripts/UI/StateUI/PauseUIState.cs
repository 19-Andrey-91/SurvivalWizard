
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class PauseUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private PauseUI _pauseUI;
        public PauseUIState(LoaderUI loaderUI, PauseUI pauseUI)
        {
            _loaderUI = loaderUI;
            _pauseUI = pauseUI;
        }

        public void Enter()
        {
            _pauseUI.gameObject.SetActive(true);
            _pauseUI.ContinueGameButton.onClick.AddListener(ContinueGame);
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            _pauseUI.ContinueGameButton.onClick.RemoveListener(ContinueGame);
            _pauseUI.gameObject.SetActive(false);
        }

        private void ContinueGame()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameUIState);
        }
    }
}
