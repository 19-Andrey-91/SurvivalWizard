using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class StartMenuUIState : IStateUI
    {
        private readonly LoaderUI _loaderUI;
        private readonly StartMenuUI _startMenuUI;

        public StartMenuUIState(LoaderUI loaderUI, StartMenuUI startMenuUI)
        {
            _loaderUI = loaderUI;
            _startMenuUI = startMenuUI;
        }
        public void Enter()
        {
            AudioListener.pause = true;

            _startMenuUI.gameObject.SetActive(true);
            _startMenuUI.Buttons.SetActive(true);
            _startMenuUI.StartButton.onClick.AddListener(ChangeStateToAddWeaponUI);
            _startMenuUI.OptionsButton.onClick.AddListener(ChangeStateToOptionsUI);
            _startMenuUI.ShopButton.onClick.AddListener(ChangeStateToShopUI);
        }

        public void Exit()
        {
            _startMenuUI.Buttons.SetActive(false);
            _startMenuUI.StartButton.onClick.RemoveListener(ChangeStateToAddWeaponUI);
            _startMenuUI.OptionsButton.onClick.RemoveListener(ChangeStateToOptionsUI);
            _startMenuUI.ShopButton.onClick.RemoveListener(ChangeStateToShopUI);
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
        
        private void ChangeStateToShopUI()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.ShopUIState);
        }
    }
}
