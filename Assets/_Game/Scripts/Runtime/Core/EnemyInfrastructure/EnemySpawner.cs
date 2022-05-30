using System;
using System.Collections;
using NavySpade.Core.Configs;
using NavySpade.Core.CrystalInfrastructure;
using NavySpade.Core.Extensions;
using NavySpade.Core.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NavySpade.Core.EnemyInfrastructure
{
    public class EnemySpawner : IInitializable
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;
        private readonly CrystalSpawner _crystalSpawner;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly EnemyConfig _enemyConfig;
        private readonly Transform _walkableArea;
        private readonly int _spawnCount;
        private readonly WaitForSeconds _waitForSpawnInterval;

        private int _spawnedCount;

        public event Action<int> Spawned;

        public EnemySpawner(GameObject prefab, Transform container, CrystalSpawner crystalSpawner, ICoroutineRunner coroutineRunner,
            EnemyConfig enemyConfig, Transform walkableArea, int spawnCount, float spawnInterval)
        {
            _prefab = prefab;
            _container = container;
            _crystalSpawner = crystalSpawner;
            _coroutineRunner = coroutineRunner;
            _enemyConfig = enemyConfig;
            _walkableArea = walkableArea;
            _spawnCount = spawnCount;
            _waitForSpawnInterval = new WaitForSeconds(spawnInterval);
        }

        public void Initialize()
        {
            SpawnWithRandomPositions();
        }

        private IEnumerator SpawnCoroutine()
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                var spawned = Object.Instantiate(_prefab, _container, true);
                
                var enemy = new Enemy(spawned, _crystalSpawner, _coroutineRunner, _enemyConfig.MoveSpeed,
                    _enemyConfig.MovePeriod,
                    _walkableArea);

                enemy.Initialize();
                spawned.transform.position = StrongExtensions.GetRandomNavMeshSamplePosition(_walkableArea);
                _spawnedCount++;
                Spawned?.Invoke(_spawnedCount);

                yield return _waitForSpawnInterval;
            }
        }

        private void SpawnWithRandomPositions()
        {
            _coroutineRunner.StartCoroutine(SpawnCoroutine());
        }
    }
}