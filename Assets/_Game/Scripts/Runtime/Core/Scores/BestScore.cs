using System;
using NavySpade.Core.Interfaces;
using UnityEngine;

namespace NavySpade.Core.Scores
{
    public class BestScore : ISavable, IInitializable
    {
        private int _value;
        private readonly string _bestScoreKey = "BestScore";

        public event Action<int> Changed;

        public BestScore(Score score)
        {
            score.Changed += OnScoreChanged;
        }

        private void OnScoreChanged(int newValue)
        {
            if (newValue > _value)
            {
                _value = newValue;
                Changed.Invoke(_value);
            }
        }

        public void Initialize()
        {
            Changed?.Invoke(_value);
        }

        public void Save() => PlayerPrefs.SetInt(_bestScoreKey, _value);

        public void Load()
        {
            _value = PlayerPrefs.GetInt(_bestScoreKey, 0);
        }
    }
}