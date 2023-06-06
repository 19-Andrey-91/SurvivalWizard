
using SurvivalWizard.Sounds;
using SurvivalWizard.UI.UIScripts;
using UnityEngine;
using Zenject;

namespace SurvivalWizard.UI.StateUI
{
    public class PauseUIState : IStateUI
    {
        private SoundManager _soundManager;
        private LoaderUI _loaderUI;
        private PauseUI _pauseUI;

        public PauseUIState(LoaderUI loaderUI, PauseUI pauseUI, SoundManager soundManager)
        {
            _loaderUI = loaderUI;
            _pauseUI = pauseUI;
            _soundManager = soundManager;
        }

        public void Enter()
        {
            _pauseUI.gameObject.SetActive(true);
            _pauseUI.EffectsVolume.value = _soundManager.EffectsVolume;
            _pauseUI.MusicVolume.value = _soundManager.MusicVolume;
            _pauseUI.ContinueGameButton.onClick.AddListener(ContinueGame);
            _pauseUI.EffectsVolume.onValueChanged.AddListener(_soundManager.SetVolumeEffects);
            _pauseUI.MusicVolume.onValueChanged.AddListener(_soundManager.SetVolumeMusic);
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            _pauseUI.ContinueGameButton.onClick.RemoveListener(ContinueGame);
            _pauseUI.EffectsVolume.onValueChanged.RemoveListener(_soundManager.SetVolumeEffects);
            _pauseUI.MusicVolume.onValueChanged.RemoveListener(_soundManager.SetVolumeMusic);
            _pauseUI.gameObject.SetActive(false);
        }

        private void ContinueGame()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameUIState);
        }
    }
}
