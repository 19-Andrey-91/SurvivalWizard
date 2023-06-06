using SurvivalWizard.Sounds;
using UnityEngine;
using Zenject;

namespace SurvivalWizard
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SoundManager _soundManagerPrefab;

        public override void InstallBindings()
        {
            Container
                .Bind<SoundManager>()
                .FromComponentInNewPrefab(_soundManagerPrefab)
                .AsSingle();
        }
    }
}
