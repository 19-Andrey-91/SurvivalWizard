
using SurvivalWizard.Base;
using SurvivalWizard.Spells;
using SurvivalWizard.UI.UIScripts;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class SpellUpgradeUIState : IStateUI
    {
        private readonly LoaderUI _loaderUI;
        private readonly GameManager _gameManager;
        private readonly SpellUpgradeUI _spellUpgradeUI;
        private readonly ChooseSpellUI _chooseSpellUI;
        private readonly ChooseSpellUpgradeUI _chooseSpellUpgradeUI;
        private readonly SpellBook _spellBook;
        private Spell _currentSpell;

        private Dictionary<string, SpellButtonUI> _buttonsChooseSpells;

        public SpellUpgradeUIState(LoaderUI loaderUI, SpellUpgradeUI spellUpgradeUI, GameManager gameManager)
        {
            _loaderUI = loaderUI;
            _gameManager = gameManager;
            _spellUpgradeUI = spellUpgradeUI;
            _buttonsChooseSpells = new();
            _chooseSpellUI = spellUpgradeUI.ChooseSpellUI;
            _chooseSpellUpgradeUI = spellUpgradeUI.ChooseSpellUgradeUI;
            _spellBook = gameManager.Player.SpellBook;
        }

        public void Enter()
        {
            _gameManager.Pause(true);
            _chooseSpellUpgradeUI.AddDamage.onClick.AddListener(AddInstantDamage);
            _chooseSpellUpgradeUI.AddDurationDamage.onClick.AddListener(AddDurationDamage);
            _spellUpgradeUI.gameObject.SetActive(true);
            _spellUpgradeUI.ChooseSpellUI.gameObject.SetActive(true);
            UpdateSpell();            
        }

        public void Exit()
        {
            _chooseSpellUpgradeUI.AddDamage.onClick.RemoveListener(AddInstantDamage);
            _chooseSpellUpgradeUI.AddDurationDamage.onClick.RemoveListener(AddDurationDamage);
            _spellUpgradeUI.gameObject.SetActive(false);
        }

        public void UpdateSpell()
        {
            _spellUpgradeUI.ChooseSpellUI.gameObject.SetActive(true);
            foreach (var spell in _spellBook.CurrentSpells)
            {
                if (!spell || _buttonsChooseSpells.ContainsKey(spell.NameSpell))
                {
                    continue;
                }
                var spellButton = Object.Instantiate(_chooseSpellUI.PrefabSpellButtonUI, _chooseSpellUI.SpellChoiseContainer);
                spellButton.Text.text = spell.NameSpell;
                spellButton.Image.sprite = spell.Sprite;
                _buttonsChooseSpells.Add(spell.NameSpell, spellButton);
                spellButton.ButtonChooseSpell.onClick.AddListener(NextUpgrade);

                void NextUpgrade()
                {
                    _chooseSpellUpgradeUI.gameObject.SetActive(true);
                    _currentSpell = spell;
                    _spellUpgradeUI.ChooseSpellUI.gameObject.SetActive(false);
                }
            }
        }

        private void AddInstantDamage()
        {
            _spellBook.AddInstantDamage(_currentSpell, 20);
            _chooseSpellUpgradeUI.gameObject.SetActive(false);
            ContinueGame();
        }

        private void AddDurationDamage()
        {
            _spellBook.AddDurationDamage(_currentSpell, 100, 5, 5);
            _chooseSpellUpgradeUI.gameObject.SetActive(false);
            ContinueGame();
        }

        private void ContinueGame()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameUIState);
        }
    }
}
