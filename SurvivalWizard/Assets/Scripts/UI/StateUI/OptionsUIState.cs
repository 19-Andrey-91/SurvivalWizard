
using SurvivalWizard.Base;
using SurvivalWizard.Sounds;
using SurvivalWizard.UI.UIScripts;

namespace SurvivalWizard.UI.StateUI
{
    public class OptionsUIState : IStateUI
    {
        private readonly SoundManager _soundManager;
        private readonly GameManager _gameManager;
        private readonly LoaderUI _loaderUI;
        private readonly OptionsUI _optionsUI;

        public OptionsUIState(LoaderUI loaderUI, OptionsUI optionsUI, SoundManager soundManager, GameManager gameManager)
        {
            _loaderUI = loaderUI;
            _optionsUI = optionsUI;
            _soundManager = soundManager;
            _gameManager = gameManager;
        }

        public void Enter()
        {
            _optionsUI.gameObject.SetActive(true);
            SubscribeToButtonBack();
            _optionsUI.EffectsVolume.value = _soundManager.EffectsVolume;
            _optionsUI.MusicVolume.value = _soundManager.MusicVolume;
            _optionsUI.EffectsVolume.onValueChanged.AddListener(_soundManager.SetVolumeEffects);
            _optionsUI.MusicVolume.onValueChanged.AddListener(_soundManager.SetVolumeMusic);

        }

        public void Exit()
        {
            _optionsUI.EffectsVolume.onValueChanged.RemoveListener(_soundManager.SetVolumeEffects);
            _optionsUI.MusicVolume.onValueChanged.RemoveListener(_soundManager.SetVolumeMusic);
            UnsubscribeToButtonBack();
            _optionsUI.gameObject.SetActive(false);
        }

        private void SubscribeToButtonBack()
        {
            if (_gameManager.IsPause)
            {
                _optionsUI.ButtonBack.onClick.AddListener(ChangeStateToStartMenuUI);
            }
            else
            {
                _gameManager.Pause(true);
                _optionsUI.ButtonBack.onClick.AddListener(ChangeStateToGame);
            }
        }

        private void UnsubscribeToButtonBack()
        {
            _optionsUI.ButtonBack.onClick.RemoveListener(ChangeStateToStartMenuUI);
            _optionsUI.ButtonBack.onClick.RemoveListener(ChangeStateToGame);
        }

        private void ChangeStateToStartMenuUI()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.StartMenuUIState);
        }
        private void ChangeStateToGame()
        {
            _loaderUI.StateMachineUI.ChangeState(_loaderUI.GameUIState);
        }
    }
}
