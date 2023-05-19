
using SurvivalWizard.Sounds;
using SurvivalWizard.UI.UIScripts;
using UnityEngine;

namespace SurvivalWizard.UI.StateUI
{
    public class PauseUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private PauseUI _pauseUI;

        public PauseUIState(LoaderUI loaderUI, PauseUI pauseUI)
        {
            _loaderUI = loaderUI;
            _pauseUI = pauseUI;
        }

        public void Enter()
        {
            _pauseUI.gameObject.SetActive(true);
            _pauseUI.EffectsVolume.value = SoundManager.Instance.EffectsVolume;
            _pauseUI.MusicVolume.value = SoundManager.Instance.MusicVolume;
            _pauseUI.ContinueGameButton.onClick.AddListener(ContinueGame);
            _pauseUI.ContinueGameButton.onClick.AddListener(SaveOptions);
            _pauseUI.EffectsVolume.onValueChanged.AddListener(SoundManager.Instance.SetVolumeEffects);
            _pauseUI.MusicVolume.onValueChanged.AddListener(SoundManager.Instance.SetVolumeMusic);
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            _pauseUI.ContinueGameButton.onClick.RemoveListener(ContinueGame);
            _pauseUI.ContinueGameButton.onClick.RemoveListener(SaveOptions);
            _pauseUI.EffectsVolume.onValueChanged.RemoveListener(SoundManager.Instance.SetVolumeEffects);
            _pauseUI.MusicVolume.onValueChanged.RemoveListener(SoundManager.Instance.SetVolumeMusic);
            _pauseUI.gameObject.SetActive(false);
        }

        private void ContinueGame()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameUIState);
        }

        private void SaveOptions()
        {
            SoundManager.Instance.SaveVolume();
        }
    }
}
