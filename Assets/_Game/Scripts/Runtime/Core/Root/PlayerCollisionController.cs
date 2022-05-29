
using System;
using NavySpade.Core.Interfaces;
using UnityEngine;

namespace NavySpade.Core.Root
{
    public class PlayerCollisionController : IInitializable, IDisposable
    {
        private readonly GameObject _gameObject;
        private readonly Player _player;
        private MonoBehaviourTriggerObserver _triggerObserver;
        private MonoBehaviourCollisionObserver _collisionObserver;

        public event Action CrystalCollided;
        public event Action EnemyCollided;

        public PlayerCollisionController(GameObject gameObject, Player player)
        {
            _gameObject = gameObject;
            _player = player;
        }
        
        public void Initialize()
        {
            _collisionObserver = _gameObject.AddComponent<MonoBehaviourCollisionObserver>();
            _collisionObserver.MonoBehaviourCollisionEntered += OnCustomCollisionEnter;
            
            _triggerObserver = _gameObject.AddComponent<MonoBehaviourTriggerObserver>();
            _triggerObserver.TriggerEntered += OnCustomTriggerEnter;
        }
        
        private void OnCustomTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                _player.Collect(collectable);
            }
        }

        private void OnCustomCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy _))
            {
                
            }
        }
        
        public void Dispose()
        {
            _collisionObserver.MonoBehaviourCollisionEntered -= OnCustomCollisionEnter;
            _triggerObserver.TriggerEntered -= OnCustomTriggerEnter;
        }
    }
}