
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private Button _continueGameButton;

        public Button ContinueGameButton { get => _continueGameButton; }
    }
}
