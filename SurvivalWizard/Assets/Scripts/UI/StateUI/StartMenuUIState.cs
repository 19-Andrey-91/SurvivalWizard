
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
            _startMenuUI.gameObject.SetActive(true);
            _startMenuUI.StartButton.onClick.AddListener(ChangeStateToGameUI);
            _startMenuUI.OptionsButton.onClick.AddListener(ChangeStateToOptionsUI);
        }

        public void Exit()
        {
            _startMenuUI.StartButton.onClick.RemoveListener(ChangeStateToGameUI);
            _startMenuUI.OptionsButton.onClick.RemoveListener(ChangeStateToOptionsUI);
        }

        private void ChangeStateToGameUI()
        {
            Object.Instantiate(_prefabGameManager);
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameUIState);
            _startMenuUI.gameObject.SetActive(false);
        }

        private void ChangeStateToOptionsUI()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.OptionsUIState);
        }
    }
}
