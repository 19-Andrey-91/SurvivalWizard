
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class UpgradeUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private SpellUpgradeUI _spellUpgradeUI;

        public UpgradeUIState(LoaderUI loaderUI, SpellUpgradeUI spellUpgradeUI)
        {
            _loaderUI = loaderUI;
            _spellUpgradeUI = spellUpgradeUI;
        }

        public void Enter()
        {
            Time.timeScale = 0f;
            _spellUpgradeUI.gameObject.SetActive(true);
            _spellUpgradeUI.ChooseSpellUgradeUI.OnUpgradeIsCancelled += ContinueGame;
            
        }

        public void Exit()
        {
            _spellUpgradeUI.ChooseSpellUgradeUI.OnUpgradeIsCancelled -= ContinueGame;
            _spellUpgradeUI.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        private void ContinueGame()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameUIState);
        }
    }
}
