using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace NavySpade.Core.Extensions
{
    public static class StrongExtensions
    {
        public static Vector3 GetRandomNavMeshSamplePosition(Transform walkableArea)
        {
            var localScale = walkableArea.localScale;
            var position = walkableArea.position;

            var minX = position.x - localScale.x / 2f;
            var maxX = position.x + localScale.x / 2f;
                        
            var minZ = position.x - localScale.z / 2f;
            var maxZ = position.x + localScale.z / 2f;
            
            var x = Random.Range(minX, maxX);
            var z = Random.Range(minZ, maxZ);
            
            if (NavMesh.SamplePosition(new  Vector3(x, 0, z), out var hit, 10, NavMesh.AllAreas))
            {
                return hit.position;
            }

            throw new InvalidOperationException("Generated random point is out of NavMesh area");
        }
    }
}