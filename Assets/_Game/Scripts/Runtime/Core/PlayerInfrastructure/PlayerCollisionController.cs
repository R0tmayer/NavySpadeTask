using System;
using System.Collections;
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
        private readonly WaitForSeconds _waitForCollisionPauseTime;
        private MonoBehaviourTriggerObserver _triggerObserver;
        private MonoBehaviourCollisionObserver _collisionObserver;
        private readonly DamageEffectController _damageEffectController;
        public event Action CollidedWithEnemy;

        public PlayerCollisionController(GameObject gameObject, Player player, Collider collider,
            ICoroutineRunner coroutineRunner, int pauseCollisionTime, DamageEffectController damageEffectController)
        {
            _gameObject = gameObject;
            _player = player;
            _collider = collider;
            _coroutineRunner = coroutineRunner;
            _damageEffectController = damageEffectController;
            _waitForCollisionPauseTime = new WaitForSeconds(pauseCollisionTime);
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
            _damageEffectController.ChangeMaterial();
            yield return _waitForCollisionPauseTime;
            _damageEffectController.ResetMaterial();
            _collider.enabled = true;
        }


        public void Dispose()
        {
            _collisionObserver.CollisionEntered -= OnCollisionEnter;
            _triggerObserver.TriggerEntered -= OnTriggerEnter;
        }
    }
}