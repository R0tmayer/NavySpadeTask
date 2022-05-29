using UnityEngine.AI;

namespace NavySpade.Core.Interfaces
{
    public interface INavMeshMovable
    {
        public NavMeshAgent NavMeshAgent { get; }
        public void Move();
    }
}