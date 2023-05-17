using UnityEngine;

namespace SurvivalWizard.Base
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private CustomDictionary<string, AudioClip> _sounds;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _effectsAudioSource;

        public CustomDictionary<string, AudioClip> Sounds { get => _sounds; }
        public AudioSource MusicAudioSource { get => _musicAudioSource; }
        public AudioSource EffectsAudioSource { get => _effectsAudioSource; }

        private void Start()
        {
            MusicAudioSource.clip = Sounds.GetValueDictionary("StartMusic");
            MusicAudioSource.Play();
        }

        public void SetVolumeMusic(float volume)
        {
            MusicAudioSource.volume = volume;
        }

        public void SetVolumeEffects(float volume)
        {
            EffectsAudioSource.volume = volume;
        }
    }
}
