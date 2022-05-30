using NavySpade.Core.Interfaces;
using TMPro;
using UnityEngine;

namespace NavySpade.Core.Scores
{
    public class ScoreView : MonoBehaviour, IInitializable
    {
        [SerializeField] private TMP_Text _text;
        private Score _score;

        public void Construct(Score score)
        {
            _score = score;
        }

        public void Initialize()
        {
            _score.Changed += UpdateText;
        }

        private void UpdateText(int value)
        {
            _text.SetText($"{value}");
        }

        private void OnDisable()
        {
            _score.Changed -= UpdateText;
        }
    }
}