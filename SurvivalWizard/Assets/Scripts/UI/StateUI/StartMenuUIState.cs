
using SurvivalWizard.Base;
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class StartMenuUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private StartMenuUI _startMenuUI;
        private GameManager _gameManager;

        public StartMenuUIState(LoaderUI loaderUI, StartMenuUI startMenuUI, GameManager _gameManager)
        {
            _loaderUI = loaderUI;
            _startMenuUI = startMenuUI;
        }
        public void Enter()
        {
            AudioListener.pause = true;

            _startMenuUI.gameObject.SetActive(true);
            _startMenuUI.StartButton.onClick.AddListener(ChangeStateToAddWeaponUI);
            _startMenuUI.OptionsButton.onClick.AddListener(ChangeStateToOptionsUI);
        }

        public void Exit()
        {
            _startMenuUI.StartButton.onClick.RemoveListener(ChangeStateToAddWeaponUI);
            _startMenuUI.OptionsButton.onClick.RemoveListener(ChangeStateToOptionsUI);
        }

        private void ChangeStateToAddWeaponUI()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.AddingWeaponUIState);
            _startMenuUI.gameObject.SetActive(false);
        }

        private void ChangeStateToOptionsUI()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.OptionsUIState);
        }
    }
}
