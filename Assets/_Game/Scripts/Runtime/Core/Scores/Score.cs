using System;

namespace NavySpade.Core.Scores
{
    public class Score
    {
        private int _value;
        
        public event Action<int> Changed;

        public void IncreaseValue()
        {
            //TODO increase value from ScoreConfig
            _value++;
            Changed?.Invoke(_value);
        }
    }
}