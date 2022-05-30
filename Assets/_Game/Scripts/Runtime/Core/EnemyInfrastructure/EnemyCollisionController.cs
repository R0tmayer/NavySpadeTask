
using System;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Root;
using UnityEngine;

namespace NavySpade.Core.EnemyInfrastructure
{
    public class EnemyCollisionController : IInitializable, IDisposable
    {
        private readonly GameObject _gameObject;
        private readonly Enemy _enemy;
        private MonoBehaviourTriggerObserver _triggerObserver;
        private MonoBehaviourCollisionObserver _collisionObserver;

        public event Action CrystalCollected;

        public EnemyCollisionController(GameObject gameObject, Enemy enemy)
        {
            _gameObject = gameObject;
            _enemy = enemy;
        }

        public void Initialize()
        {
            _collisionObserver = _gameObject.AddComponent<MonoBehaviourCollisionObserver>();
            
            _triggerObserver = _gameObject.AddComponent<MonoBehaviourTriggerObserver>();
            _triggerObserver.TriggerEntered += OnTriggerEnter;                    
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                _enemy.Collect(collectable);
                CrystalCollected?.Invoke();
            }
        }

        public void Dispose()
        {
            _triggerObserver.TriggerEntered -= OnTriggerEnter;        
        }
    }
}