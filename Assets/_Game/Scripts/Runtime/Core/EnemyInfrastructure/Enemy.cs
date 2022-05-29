using System;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Root;
using UnityEngine;

namespace NavySpade.Core.EnemyInfrastructure
{
    public class Enemy : IInitializable, IDisposable
    {
        private readonly GameObject _gameObject;
        private MonoBehaviourTriggerObserver _triggerObserver;

        public Enemy(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public void Initialize()
        {
            _triggerObserver = _gameObject.AddComponent<MonoBehaviourTriggerObserver>();
            _triggerObserver.TriggerEntered += OnCustomTriggerEnter;
        }

        private void OnCustomTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }

        public void Dispose()
        {
            _triggerObserver.TriggerEntered -= OnCustomTriggerEnter;
        }
    }
}