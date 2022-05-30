using System.Collections;
using NavySpade.Core.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Core.EnemyInfrastructure
{
    public class EnemyMoveController : IInitializable
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly NavMeshAgent _agent;
        private readonly Transform _walkableArea;
        private readonly int _movePeriod;

        public EnemyMoveController(ICoroutineRunner coroutineRunner, NavMeshAgent agent, int initialSpeed, int movePeriod, Transform walkableArea)
        {
            _coroutineRunner = coroutineRunner;
            _agent = agent;
            _agent.speed = initialSpeed;
            _movePeriod = movePeriod;
            _walkableArea = walkableArea;
        }

        public void Initialize()
        {
            _coroutineRunner.StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            float timer = _movePeriod;
            while (true)
            {
                timer += Time.deltaTime;
                
                if (timer >= _movePeriod)
                {
                    timer = 0;
                    _agent.SetDestination(StrongExtensions.StrongExtensions.GetRandomNavMeshSamplePosition(_walkableArea));
                }
                
                yield return null;
            }
        }
    }
}