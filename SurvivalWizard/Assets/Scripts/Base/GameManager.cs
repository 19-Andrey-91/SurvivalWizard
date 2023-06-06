using SurvivalWizard.Enemies;
using SurvivalWizard.PlayerScripts;
using SurvivalWizard.Sounds;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SurvivalWizard.Base
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _prefabPlayer;
        [SerializeField] private Transform _pointSpawnPlayer;
        [SerializeField] private EnemySpawner _enemySpawner;
        [Header("Map")]
        [SerializeField] private GameObject _map;
        [Space]
        [SerializeField] private List<int> _weaponSelectionLevels;

        private LvlUPManager _lvlUPManager;
        private SoundManager _soundManager;
        private DiContainer _diContainer;
        private Player _player;

        public EnemySpawner EnemySpawner { get => _enemySpawner; }

        public LvlUPManager LvlUPManager { get => _lvlUPManager ??= new LvlUPManager(_player, _weaponSelectionLevels); }

        public Player Player { get => _player; }

        [Inject]
        private void Construct(SoundManager soundManager, DiContainer diContainer)
        {
            _soundManager = soundManager;
            _diContainer = diContainer;
        }

        protected void Awake()
        {
            _player = _diContainer.InstantiatePrefabForComponent<Player>(_prefabPlayer, _pointSpawnPlayer);
            _diContainer.Bind<Player>().FromInstance(_player).AsSingle();
            Instantiate(_map);
            _enemySpawner = _diContainer.InstantiatePrefabForComponent<EnemySpawner>(_enemySpawner);
            LvlUPManager.Subscribe();
        }

        private void OnDisable()
        {
            LvlUPManager.Unsubscribe();
        }

        public void Pause(bool pause)
        {
            if (pause)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }
}