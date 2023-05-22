
using UnityEngine;

namespace SurvivalWizard.UI.UIScripts
{
    public class AddingSpellUI : MonoBehaviour
    {

        [SerializeField] private SpellButtonUI _prefabSpellButtonUI;
        [SerializeField] private Transform _spellContainer;
        public SpellButtonUI PrefabSpellButtonUI { get => _prefabSpellButtonUI; }
        public Transform SpellContainer { get => _spellContainer; }
    }
}
