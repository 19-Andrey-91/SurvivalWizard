

using SurvivalWizard.Base;
using SurvivalWizard.Sounds;
using SurvivalWizard.UI.UIScripts;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SurvivalWizard.UI.StateUI
{
    public class OptionsUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private OptionsUI _optionsUI;

        public OptionsUIState(LoaderUI loaderUI, OptionsUI optionsUI)
        {
            _loaderUI = loaderUI;
            _optionsUI = optionsUI;
        }

        public void Enter()
        {
            _optionsUI.gameObject.SetActive(true);
            _optionsUI.EffectsVolume.value = SoundManager.Instance.EffectsVolume;
            _optionsUI.MusicVolume.value = SoundManager.Instance.MusicVolume;
            _optionsUI.EffectsVolume.onValueChanged.AddListener(SoundManager.Instance.SetVolumeEffects);
            _optionsUI.MusicVolume.onValueChanged.AddListener(SoundManager.Instance.SetVolumeMusic);
            _optionsUI.ButtonBack.onClick.AddListener(ChangeStateToStartMenuUI);
            _optionsUI.ButtonBack.onClick.AddListener(SaveOptions);
        }

        public void Exit()
        {
            _optionsUI.EffectsVolume.onValueChanged.RemoveListener(SoundManager.Instance.SetVolumeEffects);
            _optionsUI.MusicVolume.onValueChanged.RemoveListener(SoundManager.Instance.SetVolumeMusic);
            _optionsUI.ButtonBack.onClick.RemoveListener(ChangeStateToStartMenuUI);
            _optionsUI.ButtonBack.onClick.RemoveListener(SaveOptions);
            _optionsUI.gameObject.SetActive(false);
        }

        private void ChangeStateToStartMenuUI()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.StartMenuUIState);
        }

        private void SaveOptions()
        {
            SoundManager.Instance.SaveVolume();
        }
    }
}
