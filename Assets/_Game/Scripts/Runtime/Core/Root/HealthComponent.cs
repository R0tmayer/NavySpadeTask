using NavySpade.Core.Interfaces;
using UnityEngine;

namespace NavySpade.Core.Root
{
    public class HealthComponent : IDamagable
    {
        private int _value;

        public HealthComponent(int initialValue)
        {
            _value = initialValue;
        }

        public void ReceiveDamage(int damage)
        {
            _value -= damage;

            if (_value <= 0)
            {
                Debug.Log($"Player DEAD. Life's point is {_value}");
            }
            
        }
    }
}