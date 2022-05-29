using System;
using NavySpade.Core.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace NavySpade.Core.Root
{
    public class NavMeshRandomMovable : INavMeshMovable
    {
        [SerializeField] private Transform _walkableArea;
        [SerializeField] private NavMeshAgent _agent;
        public NavMeshAgent NavMeshAgent => _agent;

        public NavMeshRandomMovable(Transform walkableArea)
        {
            _walkableArea = walkableArea;
        }

        public void Move()
        {
            _agent.SetDestination(Extensions.GetRandomNavMeshSamplePosition(_walkableArea));
        }
    }
}