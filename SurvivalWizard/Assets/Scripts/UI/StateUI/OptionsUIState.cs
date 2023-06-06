
using SurvivalWizard.Sounds;
using SurvivalWizard.UI.UIScripts;
using Zenject;

namespace SurvivalWizard.UI.StateUI
{
    public class OptionsUIState : IStateUI
    {
        private SoundManager _soundManager;
        private LoaderUI _loaderUI;
        private OptionsUI _optionsUI;

        public OptionsUIState(LoaderUI loaderUI, OptionsUI optionsUI, SoundManager soundManager)
        {
            _loaderUI = loaderUI;
            _optionsUI = optionsUI;
            _soundManager = soundManager;
        }

        public void Enter()
        {
            _optionsUI.gameObject.SetActive(true);
            _optionsUI.EffectsVolume.value = _soundManager.EffectsVolume;
            _optionsUI.MusicVolume.value = _soundManager.MusicVolume;
            _optionsUI.EffectsVolume.onValueChanged.AddListener(_soundManager.SetVolumeEffects);
            _optionsUI.MusicVolume.onValueChanged.AddListener(_soundManager.SetVolumeMusic);
            _optionsUI.ButtonBack.onClick.AddListener(ChangeStateToStartMenuUI);
        }

        public void Exit()
        {
            _optionsUI.EffectsVolume.onValueChanged.RemoveListener(_soundManager.SetVolumeEffects);
            _optionsUI.MusicVolume.onValueChanged.RemoveListener(_soundManager.SetVolumeMusic);
            _optionsUI.ButtonBack.onClick.RemoveListener(ChangeStateToStartMenuUI);
            _optionsUI.gameObject.SetActive(false);
        }

        private void ChangeStateToStartMenuUI()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.StartMenuUIState);
        }
    }
}
