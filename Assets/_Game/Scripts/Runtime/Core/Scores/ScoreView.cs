using TMPro;
using UnityEngine;

namespace NavySpade.Core.Scores
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Construct(Score score)
        {
            score.Changed += UpdateText;
        }

        private void UpdateText(int value)
        {
            _text.SetText($"{value}");
        }
    }
}