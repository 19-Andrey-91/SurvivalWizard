
using TMPro;
using UnityEngine;

namespace SurvivalWizard
{
    public class ButtonUpgrade : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _nameUpgrade;
        [SerializeField] TextMeshProUGUI _lvlUpgrade;
        [SerializeField] TextMeshProUGUI _priceUpgrade;

        public TextMeshProUGUI NameUpgrade { get => _nameUpgrade; }
        public TextMeshProUGUI LvlUpgrade { get => _lvlUpgrade; }
        public TextMeshProUGUI PriceUpgrade { get => _priceUpgrade; }
    }
}
