using System;
using UnityEngine;

namespace NavySpade.Core.Root
{
    public class MonoBehaviourCollisionObserver : MonoBehaviour
    {
        public event Action<Collision> CollisionEntered;

        private void OnCollisionEnter(Collision collision)
        {
            CollisionEntered?.Invoke(collision);
        }
    }
}