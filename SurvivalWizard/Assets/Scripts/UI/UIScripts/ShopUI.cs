using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private ButtonUpgrade _prefabUpgradeButton;
        [SerializeField] private Transform _containerUpgradeButtons;
        [SerializeField] private TextMeshProUGUI _currentCoins;
        
        public Button BackButton { get => _backButton; }
        public ButtonUpgrade PrefabUpgradeButton { get => _prefabUpgradeButton; }
        public Transform ContainerUpgradeButtons { get => _containerUpgradeButtons; }
        public TextMeshProUGUI CurrentCoins { get => _currentCoins; }
    }
}
