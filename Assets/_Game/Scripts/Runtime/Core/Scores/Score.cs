using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NavySpade.Core.Scores
{
    public class Score
    {
        private readonly Vector2 _incomeRange;
        private int _value;

        public Score(Vector2 incomeRange)
        {
            _incomeRange = incomeRange;
        }
        
        public event Action<int> Changed;

        public void IncreaseValue()
        {
            _value += (int)Random.Range(_incomeRange.x, _incomeRange.y);
            Changed?.Invoke(_value);
        }
    }
}