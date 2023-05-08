using SurvivalWizard.Base;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class ChooseSpellUI : MonoBehaviour
    {
        [SerializeField] private SpellButtonUI _prefabSpellButtonUI;
        [SerializeField] private ChooseSpellUgradeUI _chooseSpellUpgradeUI;

        private Dictionary<string, SpellButtonUI> _buttonsChooseSpells = new();

        public void UpdateSpell()
        {
            gameObject.SetActive(true);
            foreach (var spell in GameManager.Instance.Player.SpellBook.Spells)
            {
                if (!spell || _buttonsChooseSpells.ContainsKey(spell.NameSpell))
                {
                    return;
                }
                var spellButton = Instantiate(_prefabSpellButtonUI, transform);
                spellButton.Text.text = spell.NameSpell;
                _buttonsChooseSpells.Add(spell.NameSpell, spellButton);
                spellButton.ButtonChooseSpell.onClick.AddListener(NextUpgrade);

                void NextUpgrade()
                {
                    _chooseSpellUpgradeUI.gameObject.SetActive(true);
                    _chooseSpellUpgradeUI.CurrentSpell(spell);
                    gameObject.SetActive(false);
                }
            }
        }

    }
}