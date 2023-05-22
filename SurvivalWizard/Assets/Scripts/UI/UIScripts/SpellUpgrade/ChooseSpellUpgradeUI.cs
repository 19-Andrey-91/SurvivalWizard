
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class ChooseSpellUpgradeUI : MonoBehaviour
    {
        [SerializeField] private Button _addDamage;
        [SerializeField] private Button _addDurationDamage;

        public Button AddDamage { get => _addDamage; }
        public Button AddDurationDamage { get => _addDurationDamage; }

    }
}
