using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class OptionsUI : MonoBehaviour
    {
        [SerializeField] private Button _back;
        [SerializeField] private Button _apply;
        [SerializeField] private Slider _musicVolume;
        [SerializeField] private Slider _effectsVolume;

        public Button ButtonBack { get => _back; }
        public Button ButtonApply { get => _apply; }

        public Slider MusicVolume { get => _musicVolume; }
        public Slider EffectsVolume { get => _effectsVolume; }
    }
}
