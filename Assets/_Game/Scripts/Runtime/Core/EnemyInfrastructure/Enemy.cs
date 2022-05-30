using System;
using NavySpade.Core.CrystalInfrastructure;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Root;
using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Core.EnemyInfrastructure
{
    public class Enemy : IInitializable, IDisposable
    {
        private readonly CrystalSpawner _crystalSpawner;
        private readonly EnemyCollisionController _collisionController;
        private readonly EnemyMoveController _moveController;
        private readonly AnimatorController _animatorController;

        public Enemy(GameObject gameObject, CrystalSpawner crystalSpawner, ICoroutineRunner coroutineRunner, int moveSpeed, int movePeriod,
            Transform walkableArea)
        {
            _crystalSpawner = crystalSpawner;
            _moveController = new EnemyMoveController(coroutineRunner, gameObject.GetComponent<NavMeshAgent>(), moveSpeed, movePeriod, walkableArea);
            _collisionController = new EnemyCollisionController(gameObject, this);
            _animatorController = new AnimatorController(gameObject.GetComponent<Animator>());
            
        }

        public void Initialize()
        {
            _collisionController.Initialize();
            _animatorController.SetRunAnimation();
            _moveController.Initialize();
            _collisionController.CrystalCollected += _crystalSpawner.OnCrystalCollected;
        }

        public void Collect(ICollectable collectable)
        {
            collectable.Collect();
        }

        public void Dispose()
        {
            _collisionController.Dispose();
            _collisionController.CrystalCollected -= _crystalSpawner.OnCrystalCollected;

        }
    }
}