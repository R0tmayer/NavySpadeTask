using System.Collections;
using NavySpade.Core.Interfaces;
using UnityEngine;

namespace NavySpade.Core.Root
{
    public class EnemyMovement : IInitializable
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private INavMeshMovable _navMeshRandomMovable;

        public EnemyMovement(ICoroutineRunner coroutineRunner, int initialSpeed)
        {
            _coroutineRunner = coroutineRunner;
            _navMeshRandomMovable.NavMeshAgent.speed = initialSpeed;
        }

        public void Initialize()
        {
            _coroutineRunner.StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            float timer = 3;
            while (true)
            {
                timer += Time.deltaTime;
                
                if (timer >= 1.5f)
                {
                    timer = 0;
                    _navMeshRandomMovable.Move();
                }
                
                yield return null;
            }
        }
    }
}