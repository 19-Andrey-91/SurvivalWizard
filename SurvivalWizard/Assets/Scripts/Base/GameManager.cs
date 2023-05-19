using SurvivalWizard.Enemys;
using SurvivalWizard.PlayerScripts;
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

        private Player _player;
        public Player Player { get => _player; }
        public EnemySpawner EnemySpawner { get => _enemySpawner; }

        protected override void Awake()
        {
            base.Awake();
            Instantiate(_map);
            _player = Instantiate(_prefabPlayer, _pointSpawnPlayer);
            _enemySpawner = Instantiate(_enemySpawner);
        }
    }
}