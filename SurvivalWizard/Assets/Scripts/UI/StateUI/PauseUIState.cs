
using SurvivalWizard.Base;
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class PauseUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private PauseUI _pauseUI;
        SoundManager _soundManager;

        public PauseUIState(LoaderUI loaderUI, PauseUI pauseUI)
        {
            _loaderUI = loaderUI;
            _pauseUI = pauseUI;
            _soundManager = SoundManager.Instance;
        }

        public void Enter()
        {
            _pauseUI.gameObject.SetActive(true);
            _pauseUI.EffectsVolume.value = _soundManager.EffectsAudioSource.volume;
            _pauseUI.MusicVolume.value = _soundManager.MusicAudioSource.volume;
            _pauseUI.ContinueGameButton.onClick.AddListener(ContinueGame);
            _pauseUI.ButtonApply.onClick.AddListener(ApplyVolume);
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            _pauseUI.ContinueGameButton.onClick.RemoveListener(ContinueGame);
            _pauseUI.ButtonApply.onClick.RemoveListener(ApplyVolume);
            _pauseUI.gameObject.SetActive(false);
        }

        private void ContinueGame()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameUIState);
        }

        private void ApplyVolume()
        {
            _soundManager.SetVolumeEffects(_pauseUI.EffectsVolume.value);
            _soundManager.SetVolumeMusic(_pauseUI.MusicVolume.value);
        }
    }
}
