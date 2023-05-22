
using UnityEngine;

namespace SurvivalWizard.UI.UIScripts
{
    public class SpellUpgradeUI : MonoBehaviour
    {
        [SerializeField] private ChooseSpellUI _chooseSpellUI;
        [SerializeField] private ChooseSpellUpgradeUI _chooseSpellUgradeUI;

        public ChooseSpellUpgradeUI ChooseSpellUgradeUI { get => _chooseSpellUgradeUI; }
        public ChooseSpellUI ChooseSpellUI { get => _chooseSpellUI; }
    }
}
