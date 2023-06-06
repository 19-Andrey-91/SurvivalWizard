
using SurvivalWizard.Base;
using SurvivalWizard.PlayerScripts;
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class AddingSpellUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private AddingSpellUI _addingSpellUI;
        private bool _isCreated;
        private Player _player;

        public AddingSpellUIState(LoaderUI loaderUI, AddingSpellUI addingSpellUI, Player player)
        {
            _loaderUI = loaderUI;
            _addingSpellUI = addingSpellUI;
            _player = player;
        }
        public void Enter()
        {
            _addingSpellUI.gameObject.SetActive(true);
            CreateSpellButton();
        }

        public void Exit()
        {
            _addingSpellUI.gameObject.SetActive(false);
        }

        private void ChangeStateToGame()
        {
            _player.Fire();
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameUIState);
        }


        public void CreateSpellButton()
        {
            if (_isCreated)
            {
                return;
            }
            foreach (var spell in _player.SpellBook.Spells)
            {
                var spellButton = Object.Instantiate(_addingSpellUI.PrefabSpellButtonUI, _addingSpellUI.SpellContainer);
                spellButton.Text.text = spell.NameSpell;
                spellButton.Image.sprite = spell.Sprite;
                spellButton.ButtonChooseSpell.onClick.AddListener(AddSpell);

                void AddSpell()
                {
                    _player.SpellBook.LearnSpell(spell);
                    Object.Destroy(spellButton.gameObject);
                    ChangeStateToGame();
                }
            }
            _isCreated = true;
        }
    }
}
