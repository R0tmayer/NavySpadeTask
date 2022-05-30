using System;
using System.Collections;
using NavySpade.Core.EnemyInfrastructure;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Root;
using UnityEngine;

namespace NavySpade.Core.PlayerInfrastructure
{
    public class PlayerCollisionController : IInitializable, IDisposable
    {
        private readonly GameObject _gameObject;
        private readonly Player _player;
        private readonly Collider _collider;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly int _pauseCollisionTime;
        private MonoBehaviourTriggerObserver _triggerObserver;
        private MonoBehaviourCollisionObserver _collisionObserver;
        public event Action CrystalCollected;
        public event Action CollidedWithEnemy;

        public PlayerCollisionController(GameObject gameObject, Player player, Collider collider, ICoroutineRunner coroutineRunner, int pauseCollisionTime)
        {
            _gameObject = gameObject;
            _player = player;
            _collider = collider;
            _coroutineRunner = coroutineRunner;
            _pauseCollisionTime = pauseCollisionTime;
        }
        
        public void Initialize()
        {
            _collisionObserver = _gameObject.AddComponent<MonoBehaviourCollisionObserver>();
            _collisionObserver.CollisionEntered += OnCollisionEnter;
            
            _triggerObserver = _gameObject.AddComponent<MonoBehaviourTriggerObserver>();
            _triggerObserver.TriggerEntered += OnTriggerEnter;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                _player.Collect(collectable);
                CrystalCollected?.Invoke();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out MonoBehaviourCollisionObserver _))
            {
                StartPauseCollisionCoroutine();
                CollidedWithEnemy?.Invoke();
            }
        }

        private void StartPauseCollisionCoroutine() => _coroutineRunner.StartCoroutine(PauseCollisionForSeconds());

        private IEnumerator PauseCollisionForSeconds()
        {
            _collider.enabled = false;
            yield return new WaitForSeconds(_pauseCollisionTime);
            _collider.enabled = true;
        }

        
        public void Dispose()
        {
            _collisionObserver.CollisionEntered -= OnCollisionEnter;
            _triggerObserver.TriggerEntered -= OnTriggerEnter;
        }
    }
}