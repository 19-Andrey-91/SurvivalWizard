using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _upgradeHPButton;
        [SerializeField] private Button _upgradeMoveSpeedButton;
        [SerializeField] private TextMeshProUGUI _upgradeHPPrice;
        [SerializeField] private TextMeshProUGUI _upgradeMoveSpeedPrice;

        public Button BackButton { get => _backButton; }
        public Button UpgradeHPButton { get => _upgradeHPButton; }
        public Button UpgradeMoveSpeedButton { get => _upgradeMoveSpeedButton; }
        public TextMeshProUGUI UpgradeHPPrice { get => _upgradeHPPrice; }
        public TextMeshProUGUI UpgradeMoveSpeedPrice { get => _upgradeMoveSpeedPrice; }
    }
}
