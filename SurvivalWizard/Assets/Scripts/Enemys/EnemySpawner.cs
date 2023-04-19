
using SurvivalWizard.Base;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace SurvivalWizard.Enemys
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] ObjectPool<Enemy> pool;
        [Header("Enemy Prefabs")]
        [SerializeField] private List<PoolData<Enemy>> _enemyPoolData;
        [Header("Spawn options")]
        [SerializeField] private float _spawnRadius = 10f;
        [SerializeField] private float _minDistanceToPlayer = 5f;
        [SerializeField] private float _delayBetweenSpawn;

        private List<ObjectPool<Enemy>> _enemyPools;

        private Transform _player;
        private float _timer;
        private float _allTime;
        private int _indexPool = 0;

        private void Start()
        {
            _enemyPools = new List<ObjectPool<Enemy>>();
            CreatePools();
            _player = GameManager.Instance.Player.transform;

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
                        enemy.NumberPool = numberPool;
                        enemy.OnDiedEvent += ReleaseEnemyAfterDeath;
                        return enemy;
                    },
                    enemy =>
                    {
                        enemy.gameObject.SetActive(true);
                        enemy.Hp = enemy.MaxHp;
                    },
                    enemy =>
                    {
                        enemy.gameObject.SetActive(false);
                    },
                    enemy =>
                    {
                        enemy.OnDiedEvent -= ReleaseEnemyAfterDeath;
                        Destroy(enemy);
                    },
                    false, 
                    data.PoolCount, 
                    data.PoolMaxCount
                ));
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
            _allTime += Time.deltaTime;
            _timer += Time.deltaTime;
            if (_timer <= _delayBetweenSpawn)
            {
                return;
            }

            _timer -= _delayBetweenSpawn;
            if(_allTime > 5)
            {
                _indexPool = 1;
            }
            CheckDistanceToPlayerAndSetPosition(_enemyPools[_indexPool].Get);
        }

        private void CheckDistanceToPlayerAndSetPosition(Func<Enemy> func)
        {
            Vector3 randomPoint = GetRandomPointOnNavMesh();

            if (Vector3.Distance(randomPoint, _player.position) > _minDistanceToPlayer)
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
            randomDirection += _player.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, _spawnRadius, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return Vector3.zero;
        }
    }
}
