
using UnityEngine;


namespace SurvivalWizard.UI.UIScripts
{
    public class ChooseSpellUI : MonoBehaviour
    {
        [SerializeField] private SpellButtonUI _prefabSpellButtonUI;
        [SerializeField] private ChooseSpellUpgradeUI _chooseSpellUpgradeUI;
        [SerializeField] private Transform _spellsChoiseContainer;

        public SpellButtonUI PrefabSpellButtonUI { get => _prefabSpellButtonUI; }
        public ChooseSpellUpgradeUI ChooseSpellUpgradeUI { get => _chooseSpellUpgradeUI; }
        public Transform SpellChoiseContainer { get => _spellsChoiseContainer; }
    }
}