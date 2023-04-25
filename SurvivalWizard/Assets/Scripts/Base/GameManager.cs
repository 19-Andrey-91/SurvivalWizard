using SurvivalWizard.Enemys;
using SurvivalWizard.Player;
using UnityEngine;

namespace SurvivalWizard.Base
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private Transform _pointSpawnPlayer;
        [SerializeField] private Character _prefabPlayer;
        [SerializeField] private EnemySpawner _enemySpawner;
        [Header("Map")]
        [SerializeField] private GameObject _map;

        private Character _player;
        public Character Player { get => _player; }
        public EnemySpawner EnemySpawner { get => _enemySpawner; }

        private void Awake()
        {
            Instantiate(_map);
            _player = Instantiate(_prefabPlayer, _pointSpawnPlayer);
            _enemySpawner = Instantiate(_enemySpawner);
        }
    }
}