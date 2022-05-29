using System;
using UnityEngine;

namespace NavySpade.Core.Root
{
    public class MonoBehaviourCollisionObserver : MonoBehaviour
    {
        public event Action<Collision> MonoBehaviourCollisionEntered;

        private void OnCollisionEnter(Collision collision)
        {
            MonoBehaviourCollisionEntered?.Invoke(collision);
        }
    }
}