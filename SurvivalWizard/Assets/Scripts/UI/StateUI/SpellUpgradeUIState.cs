
using SurvivalWizard.Base;
using SurvivalWizard.Spells;
using SurvivalWizard.UI.UIScripts;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class SpellUpgradeUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private SpellUpgradeUI _spellUpgradeUI;
        private ChooseSpellUI _chooseSpellUI;
        private ChooseSpellUpgradeUI _chooseSpellUpgradeUI;
        private Spell _currentSpell;
        private SpellBook _spellBook;

        private Dictionary<string, SpellButtonUI> _buttonsChooseSpells;

        public SpellUpgradeUIState(LoaderUI loaderUI, SpellUpgradeUI spellUpgradeUI)
        {
            _loaderUI = loaderUI;
            _spellUpgradeUI = spellUpgradeUI;
            _buttonsChooseSpells = new();
            _chooseSpellUI = spellUpgradeUI.ChooseSpellUI;
            _chooseSpellUpgradeUI = spellUpgradeUI.ChooseSpellUgradeUI;
        }

        public void Enter()
        {
            _spellBook = GameManager.Instance.Player.SpellBook;

            Time.timeScale = 0f;
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
            Time.timeScale = 1f;
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
