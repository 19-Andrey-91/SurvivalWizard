
using UnityEngine;

namespace SurvivalWizard.UI.UIScripts
{
    public class SpellUpgradeUI : MonoBehaviour
    {
        [SerializeField] private ChooseSpellUI _chooseSpellUI;
        [SerializeField] private ChooseSpellUgradeUI _chooseSpellUgradeUI;

        public ChooseSpellUgradeUI ChooseSpellUgradeUI { get => _chooseSpellUgradeUI; }

        private void OnEnable()
        {
            _chooseSpellUI.gameObject.SetActive(true);
            _chooseSpellUI.UpdateSpell();
        }
    }
}
