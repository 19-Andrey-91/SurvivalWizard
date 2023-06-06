using UnityEngine;
using UnityEngine.Audio;

namespace SurvivalWizard.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Transform _soundContainer;
        [SerializeField] private CustomDictionary<string, Sound> _sounds;

        private const string _musicAudioGroupVolume = "Music";
        private const string _effectsAudioGroupVolume = "Effects";
        private const string _nameSaveMusicVolume = "SavesMusicVolume";
        private const string _nameSaveEffectVolume = "SavesEffectsVolume";

        public float MusicVolume { get => LoadVolume(_nameSaveMusicVolume); }
        public float EffectsVolume { get => LoadVolume(_nameSaveEffectVolume); }

        private void Start()
        {
            PlaySound("StartMusic", true);
            SetVolumeMusic(MusicVolume);
            SetVolumeEffects(EffectsVolume);
        }

        public void SetVolumeMusic(float volume)
        {
            _audioMixer.SetFloat(_musicAudioGroupVolume, Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat(_nameSaveMusicVolume, volume);
        }

        public void SetVolumeEffects(float volume)
        {
            _audioMixer.SetFloat(_effectsAudioGroupVolume, Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat(_nameSaveEffectVolume, volume);
        }

        public AudioSource PlaySound(string name, bool ignorePause = false)
        {
            var audioSource = CreateSound(name, _soundContainer);
            if (ignorePause)
            {
                audioSource.ignoreListenerPause = ignorePause;
            }
            audioSource.Play();
            return audioSource;
        }

        public AudioSource PlaySound(string name, Vector3 position, bool ignorePause = false)
        {
            var audioSource = PlaySound(name, ignorePause);
            audioSource.transform.position = position;
            return audioSource;
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

        private float LoadVolume(string nameSave)
        {
            return PlayerPrefs.GetFloat(nameSave, 1f);
        }
    }
}
