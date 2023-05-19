using UnityEngine;
using UnityEngine.Audio;

namespace SurvivalWizard.Sounds
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Transform _soundContainer;
        [SerializeField] private CustomDictionary<string, Sound> _sounds;

        private const string _musicAudioGroupVolume = "Music";
        private const string _effectsAudioGroupVolume = "Effects";
        private const string _nameSaveMusicVolume = "SavesMusicVolume";
        private const string _nameSaveEffectVolume = "SavesEffectsVolume";

        public float MusicVolume { get; private set; }
        public float EffectsVolume { get; private set; }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            PlaySound("StartMusic");
            TryLoadVolume();
            SetVolumeMusic(MusicVolume);
            SetVolumeEffects(EffectsVolume);
        }

        public void SetVolumeMusic(float volume)
        {
            _audioMixer.SetFloat(_musicAudioGroupVolume, Mathf.Log10(volume) * 20);
            MusicVolume = volume;
        }

        public void SetVolumeEffects(float volume)
        {
            _audioMixer.SetFloat(_effectsAudioGroupVolume, Mathf.Log10(volume) * 20);
            EffectsVolume = volume;
        }

        public void PlaySound(string name)
        {
            var audioSource = CreateSound(name, _soundContainer);
            audioSource.Play();
        }

        public void PlaySound(string name, Vector3 position)
        {
            var audioSource = CreateSound(name, _soundContainer);
            audioSource.transform.position = position;
            audioSource.Play();
        }

        private AudioSource CreateSound(string name, Transform _soundContainer)
        {
            var sound = _sounds.GetValue(name);
            var audioSource = Instantiate(sound.AudioSource, _soundContainer);
            audioSource.clip = sound.AudioClip;

            if (!audioSource.loop)
            {
                Destroy(audioSource.gameObject, audioSource.clip.length);
            }
            return audioSource;
        }

        public void SaveVolume()
        {
            PlayerPrefs.SetFloat(_nameSaveMusicVolume, MusicVolume);
            PlayerPrefs.SetFloat(_nameSaveEffectVolume, EffectsVolume);
        }

        private bool TryLoadVolume()
        {
            if (PlayerPrefs.HasKey(_nameSaveMusicVolume))
            {
                MusicVolume = PlayerPrefs.GetFloat(_nameSaveMusicVolume);
                EffectsVolume = PlayerPrefs.GetFloat(_nameSaveEffectVolume);
                return true;
            }
            MusicVolume = 1f;
            EffectsVolume = 1f;
            return false;
        }
    }
}
