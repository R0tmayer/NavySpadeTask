
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

        public EnemyCollisionController(GameObject gameObject, Enemy enemy)
        {
            _gameObject = gameObject;
            _enemy = enemy;
        }

        public void Initialize()
        {
            _triggerObserver = _gameObject.AddComponent<MonoBehaviourTriggerObserver>();
            _triggerObserver.TriggerEntered += OnTriggerEnter;        
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                _enemy.Collect(collectable);
            }
        }

        public void Dispose()
        {
            _triggerObserver.TriggerEntered -= OnTriggerEnter;        
        }
    }
}