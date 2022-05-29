using System;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Old;
using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Core.Root
{
    [Serializable]
    public class PlayerMoveController : ITickable
    {
        private NavMeshAgent _agent;
        private Camera _camera;

        public event Action DestinationReached;
        public event Action StillMoving;

        public PlayerMoveController(NavMeshAgent agent, Camera camera, int initialSpeed)
        {
            _agent = agent;
            _agent.speed = initialSpeed;
            _camera = camera;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, float.MaxValue))
                {
                    _agent.SetDestination(hit.point);
                }
            }
        }
        
        private void CheckDestinationReached()
        {
            if (_agent.pathPending)
            {
                StillMoving?.Invoke();
                return;
            }

            if (_agent.pathStatus == NavMeshPathStatus.PathComplete &&
                _agent.remainingDistance <= 0.05f)
            {
                DestinationReached?.Invoke();
            }
        }
    }
}