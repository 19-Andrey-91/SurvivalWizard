
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class StartMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private GameObject _buttons;

        public Button StartButton { get => _startButton; }
        public Button OptionsButton { get => _optionsButton; }
        public Button ShopButton { get => _shopButton; }
        public GameObject Buttons { get => _buttons; }
    }
}
