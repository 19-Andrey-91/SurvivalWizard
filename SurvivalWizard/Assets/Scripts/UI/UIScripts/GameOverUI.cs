
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        public Button RestartButton { get => _restartButton; }
    }
}
