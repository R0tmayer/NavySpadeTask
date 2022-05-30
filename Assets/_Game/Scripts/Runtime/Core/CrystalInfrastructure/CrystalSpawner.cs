using System;
using System.Collections;
using NavySpade.Core.Extensions;
using NavySpade.Core.Interfaces;
using NavySpade.Core.PlayerInfrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NavySpade.Core.CrystalInfrastructure
{
    public class CrystalSpawner : IInitializable, IDisposable
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;
        private readonly Player _player;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly Transform _walkableArea;
        private readonly int _startSpawnCount;
        private readonly int _additionalSpawnCount;
        private readonly WaitForSeconds _waitForSpawnInterval;

        private int _crystals;
        public event Action<int>Spawned;
        public event Action<int> CrystalCollected;

        public CrystalSpawner(GameObject prefab, Transform container, Player player, ICoroutineRunner coroutineRunner, Transform walkableArea, int startSpawnCount, int additionalSpawnCount, float additionalSpawnInterval)
        {
            _prefab = prefab;
            _container = container;
            _player = player;
            _coroutineRunner = coroutineRunner;
            _walkableArea = walkableArea;
            _startSpawnCount = startSpawnCount;
            _additionalSpawnCount = additionalSpawnCount;
            _waitForSpawnInterval = new WaitForSeconds(additionalSpawnInterval);
        }

        public void Initialize()
        {
            _player.ItemCollected += OnCrystalCollected;
            SpawnWithRandomPositions();
        }

        public void OnCrystalCollected()
        {
            _crystals--;
            CrystalCollected?.Invoke(_crystals);
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
            spawned.transform.position = StrongExtensions.GetRandomNavMeshSamplePosition(_walkableArea);

            var newY = spawned.GetComponent<Collider>().bounds.size.y;
            var newPosition = spawned.transform.position;
            newPosition.y = newY;
            spawned.transform.position = newPosition;

            _crystals++;
            Spawned?.Invoke(_crystals);
        }

        private void SpawnWithRandomPositions()
        {
            _coroutineRunner.StartCoroutine(SpawnCoroutine());
        }

        public void Dispose()
        {
            _player.ItemCollected -= OnCrystalCollected;

        }
    }
}