using NavySpade.Core.Configs;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Root;
using UnityEngine;

namespace NavySpade.Core.EnemyInfrastructure
{
    public class EnemySpawner : IInitializable
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly EnemyConfig _enemyConfig;
        private readonly Transform _walkableArea;
        private readonly int _spawnCount;

        public EnemySpawner(GameObject prefab, Transform container, ICoroutineRunner coroutineRunner,
            EnemyConfig enemyConfig, Transform walkableArea, int spawnCount)
        {
            _prefab = prefab;
            _container = container;
            _coroutineRunner = coroutineRunner;
            _enemyConfig = enemyConfig;
            _walkableArea = walkableArea;
            _spawnCount = spawnCount;
        }

        public void Initialize()
        {
            SpawnWithRandomPositions();
        }

        private void SpawnWithRandomPositions()
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                var spawned = Object.Instantiate(_prefab, _container, true);
                
                var enemy = new Enemy(spawned, _coroutineRunner, _enemyConfig.MoveSpeed,
                    _enemyConfig.MovePeriod,
                    _walkableArea);

                enemy.Initialize();

                spawned.transform.position = Extensions.GetRandomNavMeshSamplePosition(_walkableArea);
            }
        }
    }
}