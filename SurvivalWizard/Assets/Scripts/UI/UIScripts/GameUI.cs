
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countKillsText;
        [SerializeField] private TextMeshProUGUI _countLevelText;
        [SerializeField] private Button _pauseButton;
        [Header("XpBar")]
        [SerializeField] private TextMeshProUGUI _xpBarText;
        [SerializeField] private Image _xpBar;
        [Header("HpBar")]
        [SerializeField] private TextMeshProUGUI _hpBarText;
        [SerializeField] private Image _hpBar;

        public Button PauseButton { get => _pauseButton; }
        public TextMeshProUGUI HPBarText { get => _hpBarText; }
        public TextMeshProUGUI XPBarText { get => _xpBarText; }
        public TextMeshProUGUI CountKillsText { get => _countKillsText; }
        public TextMeshProUGUI CountLevelText { get => _countLevelText; }
        public Image HPBarImage { get => _hpBar; }
        public Image XPBarImage { get => _xpBar; }
    }
}
