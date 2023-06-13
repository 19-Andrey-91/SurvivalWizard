using SurvivalWizard.Enemies;
using SurvivalWizard.PlayerScripts;
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
        private DiContainer _diContainer;

        public EnemySpawner EnemySpawner { get => _enemySpawner; }
        public LvlUPManager LvlUPManager { get => _lvlUPManager ??= new LvlUPManager(Player, _weaponSelectionLevels); }
        public Player Player { get; private set; }
        public bool IsPause { get => Time.timeScale == 0; }

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        protected void Awake()
        {
            Player = _diContainer.InstantiatePrefabForComponent<Player>(_prefabPlayer, _pointSpawnPlayer);
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