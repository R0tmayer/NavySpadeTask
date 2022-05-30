﻿using System;
using NavySpade.Core.Configs;
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

        public event Action ItemCollected;

        public Player(GameObject gameObject, Camera camera, PlayerConfig playerConfig)
        {
            _moveController =
                new PlayerMoveController(gameObject.GetComponent<NavMeshAgent>(), camera, playerConfig.MoveSpeed);
            _collisionController = new PlayerCollisionController(gameObject, this);
            _animatorController = new AnimatorController(gameObject.GetComponent<Animator>());
        }

        public void Initialize()
        {
            _collisionController.Initialize();
            _moveController.MoveStarted += _animatorController.SetRunAnimation;
            _moveController.DestinationReached += _animatorController.SetIdleAnimation;
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

            _collisionController.Dispose();
        }
    }
}