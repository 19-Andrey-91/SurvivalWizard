
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class StartMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        
        public Button StartButton { get => _startButton; }
    }
}
