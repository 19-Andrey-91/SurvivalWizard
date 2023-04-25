
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hpBarText;
        [SerializeField] private TextMeshProUGUI _countKilllsText;
        [SerializeField] private Image _hpBar;
        [SerializeField] private Button _pauseButton;

        public Button PauseButton { get => _pauseButton; }
        public TextMeshProUGUI HPBarText { get => _hpBarText; }
        public TextMeshProUGUI CountKillsText { get => _countKilllsText; }
        public Image HPBarImage { get => _hpBar; }
    }
}
