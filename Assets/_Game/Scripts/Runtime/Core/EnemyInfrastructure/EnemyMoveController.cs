using System.Collections;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Root;
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

        public EnemyMoveController(ICoroutineRunner coroutineRunner, NavMeshAgent agent, int initialSpeed, Transform walkableArea, int movePeriod)
        {
            _coroutineRunner = coroutineRunner;
            _agent = agent;
            _walkableArea = walkableArea;
            _movePeriod = movePeriod;
            _agent.speed = initialSpeed;
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
                    _agent.SetDestination(Extensions.GetRandomNavMeshSamplePosition(_walkableArea));
                }
                
                yield return null;
            }
        }
    }
}