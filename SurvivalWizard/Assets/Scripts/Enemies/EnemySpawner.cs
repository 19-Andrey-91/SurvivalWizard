
using SurvivalWizard.Base;
using SurvivalWizard.PlayerScripts;
using SurvivalWizard.Sounds;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using Zenject;

namespace SurvivalWizard.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Enemy Prefabs")]
        [SerializeField] private List<PoolData<Enemy>> _enemyPoolData;
        [Header("Spawn options")]
        [SerializeField] private float _spawnRadius = 10f;
        [SerializeField] private float _minDistanceToPlayer = 5f;
        [SerializeField] private float _delayBetweenSpawn;
        [SerializeField] private float _enemyWaveChangeTime;
        [Header("Upgrade")]
        [SerializeField] private int _killToUpgrade;
        [SerializeField] private int _killToAddingWeapon;

        private List<ObjectPool<Enemy>> _enemyPools;

        private Player _player;
        private float _spawnTimer;
        private float _waveTimer;
        private int _indexPool = 0;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        private void Start()
        {
            _enemyPools = new List<ObjectPool<Enemy>>();
            CreatePools();
        }

        private void CreatePools()
        {
            for (int i = 0; i < _enemyPoolData.Count; i++)
            {
                PoolData<Enemy> data = _enemyPoolData[i];
                int numberPool = i;

                _enemyPools.Add(new ObjectPool<Enemy>(
                    () =>
                    {
                        Enemy enemy = Instantiate(data.Prefab, data.Container);
                        enemy.Initialize(_player);
                        enemy.NumberPool = numberPool;
                        enemy.OnDiedEvent += ReleaseEnemyAfterDeath;
                        enemy.OnDiedEvent += EntityDeath;
                        return enemy;
                    },
                    enemyGet =>
                    {
                        enemyGet.Hp = enemyGet.MaxHp;
                        enemyGet.gameObject.SetActive(true);
                    },
                    enemyRelease =>
                    {
                        enemyRelease.gameObject.SetActive(false);
                    },
                    enemyDestroy =>
                    {
                        enemyDestroy.OnDiedEvent -= EntityDeath;
                        enemyDestroy.OnDiedEvent -= ReleaseEnemyAfterDeath;
                        Destroy(enemyDestroy.gameObject);
                    },
                    true,
                    data.PoolCount,
                    data.PoolMaxCount
                ));
            }
        }

        private void EntityDeath(Entity obj)
        {
            if (obj is Enemy enemy)
            {
                Bank.Instance.AddCoins(enemy.NumberOfCoins);
                _player.PlayerLevel.AddExperience(enemy.NumberOfExperience);
            }
        }

        private void ReleaseEnemyAfterDeath(Entity obj)
        {
            if (obj is Enemy enemy)
            {
                _enemyPools[enemy.NumberPool].Release(enemy);
            }
        }

        private void Update()
        {
            _waveTimer += Time.deltaTime;
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer <= _delayBetweenSpawn)
            {
                return;
            }
            _spawnTimer -= _delayBetweenSpawn;
            ChangePoolIndex();
            CheckDistanceToPlayerAndSetPosition(_enemyPools[_indexPool].Get);
        }

        private void ChangePoolIndex()
        {
            if (_waveTimer > _enemyWaveChangeTime)
            {
                _waveTimer -= _enemyWaveChangeTime;
                if (_indexPool >= _enemyPools.Count - 1)
                {
                    _indexPool = 0;
                    return;
                }
                _indexPool++;
            }
        }

        private void CheckDistanceToPlayerAndSetPosition(Func<Enemy> func)
        {
            Vector3 randomPoint = GetRandomPointOnNavMesh();

            if (Vector3.Distance(randomPoint, _player.transform.position) > _minDistanceToPlayer)
            {
                if (randomPoint != Vector3.zero)
                {
                    func().EnemyTransform.position = randomPoint;
                }
            }
        }

        private Vector3 GetRandomPointOnNavMesh()
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _spawnRadius;
            randomDirection += _player.transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, _spawnRadius, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return Vector3.zero;
        }
    }
}
