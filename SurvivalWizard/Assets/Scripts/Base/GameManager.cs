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

        private Character _player;
        public Character Player { get => _player; }

        private void Awake()
        {
            _player = Instantiate(_prefabPlayer, _pointSpawnPlayer);
            Instantiate(_enemySpawner);
        }
    }
}