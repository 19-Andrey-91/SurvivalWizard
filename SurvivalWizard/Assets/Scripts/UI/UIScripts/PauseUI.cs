
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private Button _continueGameButton;
        [SerializeField] private Button _apply;
        [SerializeField] private Slider _musicVolume;
        [SerializeField] private Slider _effectsVolume;

        public Button ContinueGameButton { get => _continueGameButton; }
        public Button ButtonApply { get => _apply; }
        public Slider MusicVolume { get => _musicVolume; }
        public Slider EffectsVolume { get => _effectsVolume; }
    }
}
