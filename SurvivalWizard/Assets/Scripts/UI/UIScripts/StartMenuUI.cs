
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class StartMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionsButton;
        
        public Button StartButton { get => _startButton; }
        public Button OptionsButton { get => _optionsButton;}
    }
}
