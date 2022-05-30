using System;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Root;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace NavySpade.Core.EnemyInfrastructure
{
    public class Enemy : IInitializable, IDisposable
    {
        private readonly EnemyCollisionController _collisionController;
        private readonly EnemyMoveController _moveController;
        private readonly AnimatorController _animatorController;

        public Enemy(GameObject gameObject, ICoroutineRunner coroutineRunner, int moveSpeed, int movePeriod,
            Transform walkableArea)
        {
            _moveController = new EnemyMoveController(coroutineRunner, gameObject.GetComponent<NavMeshAgent>(), moveSpeed, movePeriod, walkableArea);
            _collisionController = new EnemyCollisionController(gameObject, this);
            _animatorController = new AnimatorController(gameObject.GetComponent<Animator>());
            
        }

        public void Initialize()
        {
            _collisionController.Initialize();
            _animatorController.SetRunAnimation();
            _moveController.Initialize();
        }

        public void Collect(ICollectable collectable)
        {
            collectable.Collect();
        }

        public void Dispose()
        {
            _collisionController.Dispose();
        }
    }
}