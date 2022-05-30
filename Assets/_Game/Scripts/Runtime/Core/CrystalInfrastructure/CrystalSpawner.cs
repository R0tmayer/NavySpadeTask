using System;
using System.Collections;
using NavySpade.Core.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NavySpade.Core.CrystalInfrastructure
{
    public class CrystalSpawner : IInitializable
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly Transform _walkableArea;
        private readonly int _startSpawnCount;
        private readonly int _additionalSpawnCount;
        private readonly WaitForSeconds _waitForSpawnInterval;

        private int _spawnedCount;
        public event Action<int>Spawned;

        public CrystalSpawner(GameObject prefab, Transform container, ICoroutineRunner coroutineRunner, Transform walkableArea, int startSpawnCount, int additionalSpawnCount, float additionalSpawnInterval)
        {
            _prefab = prefab;
            _container = container;
            _coroutineRunner = coroutineRunner;
            _walkableArea = walkableArea;
            _startSpawnCount = startSpawnCount;
            _additionalSpawnCount = additionalSpawnCount;
            _waitForSpawnInterval = new WaitForSeconds(additionalSpawnInterval);
        }

        public void Initialize()
        {
            SpawnWithRandomPositions();
        }

        private IEnumerator SpawnCoroutine()
        {
            for (int i = 0; i < _startSpawnCount; i++)
            {
                SpawnSingleCrystal();
            }            
            
            for (int i = 0; i < _additionalSpawnCount; i++)
            {
                yield return _waitForSpawnInterval;
                SpawnSingleCrystal();
            }
        }

        private void SpawnSingleCrystal()
        {
            var spawned = Object.Instantiate(_prefab, _container, true);
            spawned.transform.position = StrongExtensions.StrongExtensions.GetRandomNavMeshSamplePosition(_walkableArea);

            var newY = spawned.GetComponent<Collider>().bounds.size.y;
            var newPosition = spawned.transform.position;
            newPosition.y = newY;
            spawned.transform.position = newPosition;

            _spawnedCount++;
            Spawned?.Invoke(_spawnedCount);
        }

        private void SpawnWithRandomPositions()
        {
            _coroutineRunner.StartCoroutine(SpawnCoroutine());
        }
    }
}