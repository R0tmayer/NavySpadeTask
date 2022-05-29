using TMPro;
using UnityEngine;

namespace NavySpade.Core.Scores
{
    public class BestScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private BestScore _bestScore;

        public void Construct(BestScore bestScore)
        {
            _bestScore = bestScore;
            _bestScore.Changed += UpdateText;
        }

        private void UpdateText(int value)
        {
            _text.SetText($"Best score is {value}");
        }

        private void OnDisable()
        {
            _bestScore.Changed -= UpdateText;
        }
    }
}