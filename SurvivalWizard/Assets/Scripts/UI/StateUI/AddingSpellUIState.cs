
using SurvivalWizard.Base;
using SurvivalWizard.PlayerScripts;
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class AddingSpellUIState : IStateUI
    {
        private readonly LoaderUI _loaderUI;
        private readonly AddingSpellUI _addingSpellUI;
        private readonly Player _player;
        private readonly GameManager _gameManager;
        private bool _isCreated;

        public AddingSpellUIState(LoaderUI loaderUI, AddingSpellUI addingSpellUI, GameManager gameManager)
        {
            _loaderUI = loaderUI;
            _addingSpellUI = addingSpellUI;
            _player = gameManager.Player;
            _gameManager = gameManager;
        }

        public void Enter()
        {
            _gameManager.Pause(true);
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
