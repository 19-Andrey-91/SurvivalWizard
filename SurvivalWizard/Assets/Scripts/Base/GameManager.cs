using SurvivalWizard.Enemies;
using SurvivalWizard.PlayerScripts;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalWizard.Base
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private Transform _pointSpawnPlayer;
        [SerializeField] private Player _prefabPlayer;
        [SerializeField] private EnemySpawner _enemySpawner;
        [Header("Map")]
        [SerializeField] private GameObject _map;
        [Space]
        [SerializeField] private List<int> _weaponSelectionLevels;

        private LvlUPManager _lvlUPManager;
        private Player _player;
        public Player Player { get => _player; }
        public EnemySpawner EnemySpawner { get => _enemySpawner; }

        public LvlUPManager LvlUPManager { get => _lvlUPManager; }

        protected override void Awake()
        {
            base.Awake();
            Instantiate(_map);
            _player = Instantiate(_prefabPlayer, _pointSpawnPlayer);
            _enemySpawner = Instantiate(_enemySpawner);
            _lvlUPManager = new LvlUPManager(this, _weaponSelectionLevels);
            _lvlUPManager.Subscribe();
        }

        private void OnDisable()
        {
            _lvlUPManager.Unsubscribe();
        }
    }
}