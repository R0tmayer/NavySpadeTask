using System;
using UnityEngine;

namespace NavySpade.Core.Root
{
    public class MonoBehaviourTriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerExited;
        public event Action<Collider> TriggerEntered;
        public event Action<Collider> TriggerStay;
        
        private void OnTriggerEnter(Collider other)
        {
            TriggerEntered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExited?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            TriggerStay?.Invoke(other);
        }
    }
}