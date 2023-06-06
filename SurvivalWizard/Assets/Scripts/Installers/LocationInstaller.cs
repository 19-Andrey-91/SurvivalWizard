using SurvivalWizard.Base;
using SurvivalWizard.PlayerScripts;
using UnityEngine;
using Zenject;

namespace SurvivalWizard
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManagerPrefab;

        public override void InstallBindings()
        {
            Container
                .Bind<GameManager>()
                .FromComponentInNewPrefab(_gameManagerPrefab)
                .AsSingle();
        }
    }
}
