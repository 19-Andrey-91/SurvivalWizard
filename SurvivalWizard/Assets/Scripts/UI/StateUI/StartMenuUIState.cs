
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
            _startMenuUI.StartButton.onClick.AddListener(CreateGameManager);
        }

        public void Exit()
        {
            _startMenuUI.StartButton.onClick.RemoveListener(CreateGameManager);
            _startMenuUI.gameObject.SetActive(false);
        }

        private void CreateGameManager()
        {
            Object.Instantiate(_prefabGameManager);
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameUIState);
        }
    }
}
