using System;
using NavySpade.Core.Interfaces;
using UnityEngine;

namespace NavySpade.Core.Health
{
    public class HealthComponent : IDamagable
    {
        private readonly int _initialValue;
        public event Action<int> Changed;
        public event Action Died;

        public HealthComponent(int initialValue)
        {
            _initialValue = initialValue;
            Value = initialValue;
        }

        public int Value { get; private set; }

        public void IncreaseValue()
        {
            if (Value >= _initialValue)
                return;

            Value++;
            Changed?.Invoke(Value);
        }

        public void ReceiveDamage()
        {
            Value--;

            Changed?.Invoke(Value);

            if (Value <= 0)
            {
                Died?.Invoke();
                Debug.Log($"Player DEAD. Life's point is {Value}");
            }
        }
    }
}