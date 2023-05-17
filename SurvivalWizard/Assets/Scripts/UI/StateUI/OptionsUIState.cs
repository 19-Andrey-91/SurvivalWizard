

using SurvivalWizard.Base;
using SurvivalWizard.UI.UIScripts;

namespace SurvivalWizard.UI.StateUI
{
    public class OptionsUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private OptionsUI _optionsUI;
        SoundManager _soundManager;

        public OptionsUIState(LoaderUI loaderUI, OptionsUI optionsUI)
        {
            _loaderUI = loaderUI;
            _optionsUI = optionsUI;
            _soundManager = SoundManager.Instance;
        }

        public void Enter()
        {
            _optionsUI.EffectsVolume.value = _soundManager.EffectsAudioSource.volume;
            _optionsUI.MusicVolume.value = _soundManager.MusicAudioSource.volume;
            _optionsUI.gameObject.SetActive(true);
            _optionsUI.ButtonApply.onClick.AddListener(ApplyVolume);
            _optionsUI.ButtonBack.onClick.AddListener(ChangeStateToStartMenuUI);
        }

        public void Exit()
        {
            _optionsUI.ButtonApply.onClick.RemoveListener(ApplyVolume);
            _optionsUI.ButtonBack.onClick.RemoveListener(ChangeStateToStartMenuUI);
            _optionsUI.gameObject.SetActive(false);
        }

        private void ApplyVolume()
        {
            _soundManager.SetVolumeEffects(_optionsUI.EffectsVolume.value);
            _soundManager.SetVolumeMusic(_optionsUI.MusicVolume.value);
        }

        private void ChangeStateToStartMenuUI()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.StartMenuUIState);
        }
    }
}
