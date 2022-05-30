using System;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Core.PlayerInfrastructure
{
    [Serializable]
    public class PlayerMoveController : IInitializable, ITickable, IDisposable
    {
        private NavMeshAgent _agent;
        private readonly Mediator _mediator;
        private Camera _camera;

        private bool _canTouch;

        public event Action DestinationReached;
        public event Action MoveStarted;

        public PlayerMoveController(NavMeshAgent agent, Mediator mediator, Camera camera, int initialSpeed)
        {
            _agent = agent;
            _mediator = mediator;
            _agent.speed = initialSpeed;
            _camera = camera;
        }

        public void Tick()
        {
            if (_canTouch && Input.GetMouseButtonDown(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, float.MaxValue))
                {
                    MoveStarted?.Invoke();
                    _agent.SetDestination(hit.point);
                }
            }
        }

        public void OnGameHasStarted()
        {
            _canTouch = true;
            Time.timeScale = 1;
        }

        private void CheckDestinationReached()
        {
            if (_agent.pathPending)
                return;

            if (_agent.pathStatus == NavMeshPathStatus.PathComplete &&
                _agent.remainingDistance <= 0.05f)
            {
                DestinationReached?.Invoke();
            }
        }

        public void Initialize()
        {
            _mediator.GameStarted += OnGameHasStarted;
        }

        public void Dispose()
        {
            _mediator.GameStarted -= OnGameHasStarted;
        }
    }
}