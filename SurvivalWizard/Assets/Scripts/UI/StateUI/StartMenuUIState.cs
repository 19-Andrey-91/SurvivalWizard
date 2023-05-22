
using SurvivalWizard.Base;
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class StartMenuUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private StartMenuUI _startMenuUI;
        private GameManager _prefabGameManager;
        public StartMenuUIState(LoaderUI loaderUI, StartMenuUI startMenuUI, GameManager prefabGameManager)
        {
            _loaderUI = loaderUI;
            _startMenuUI = startMenuUI;
            _prefabGameManager = prefabGameManager;
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
            Object.Instantiate(_prefabGameManager);
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.AddingWeaponUIState);
            _startMenuUI.gameObject.SetActive(false);
        }

        private void ChangeStateToOptionsUI()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.OptionsUIState);
        }
    }
}
