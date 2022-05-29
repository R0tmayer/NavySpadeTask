using NavySpade.Core.Interfaces;
using UnityEngine;

namespace NavySpade.Core.Root
{
    public class EnemySpawner : IInitializable
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly Transform _walkableArea;
        private readonly int _spawnCount;

        public EnemySpawner(EnemyFactory enemyFactory, Transform walkableAreaCollider, int spawnCount)
        {
            _enemyFactory = enemyFactory;
            _walkableArea = walkableAreaCollider;
            _spawnCount = spawnCount;
        }

        public void Initialize()
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                var spawned = _enemyFactory.GetNewInstance();
                spawned.transform.position = Extensions.GetRandomNavMeshSamplePosition(_walkableArea);
                
                if(spawned.TryGetComponent(out EnemyMovement enemyMovement))
                    enemyMovement.Initialize();
            }
        }
    }
}