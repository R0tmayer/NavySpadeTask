using System;
using NavySpade.Core.Configs;
using NavySpade.Core.Health;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Root;
using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Core.PlayerInfrastructure
{
    public class Player : IInitializable, ITickable, IDisposable
    {
        private readonly PlayerMoveController _moveController;
        private readonly PlayerCollisionController _collisionController;
        private readonly AnimatorController _animatorController;

        public HealthComponent HealthComponent { get; }

        public event Action ItemCollected;


        public Player(GameObject gameObject, Camera camera, SkinnedMeshRenderer skinnedMeshRenderer, PlayerConfig playerConfig, ICoroutineRunner coroutineRunner)
        {
            _moveController =
                new PlayerMoveController(gameObject.GetComponent<NavMeshAgent>(), camera, playerConfig.MoveSpeed);
            
            var damageEffectController = new DamageEffectController(skinnedMeshRenderer, playerConfig.InvincibleMaterial);
            
            _collisionController = new PlayerCollisionController(gameObject, this, gameObject.GetComponent<Collider>(), coroutineRunner, playerConfig.PauseCollisionTime, damageEffectController);
            
            _animatorController = new AnimatorController(gameObject.GetComponent<Animator>());
            HealthComponent = new HealthComponent(playerConfig.Health);
        }

        public void Initialize()
        {
            _collisionController.Initialize();
            _moveController.MoveStarted += _animatorController.SetRunAnimation;
            _moveController.DestinationReached += _animatorController.SetIdleAnimation;
            _collisionController.CollidedWithEnemy += HealthComponent.ReceiveDamage;
        }

        public void Collect(ICollectable collectable)
        {
            collectable.Collect();
            ItemCollected?.Invoke();
        }

        public void Tick()
        {
            _moveController.Tick();
        }

        public void Dispose()
        {
            _moveController.MoveStarted -= _animatorController.SetRunAnimation;
            _moveController.DestinationReached -= _animatorController.SetIdleAnimation;
            _collisionController.CollidedWithEnemy -= HealthComponent.ReceiveDamage;


            _collisionController.Dispose();
        }
    }
}