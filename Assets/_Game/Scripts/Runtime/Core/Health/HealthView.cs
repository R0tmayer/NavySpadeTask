using System.Linq;
using NavySpade.Core.Scores;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NavySpade.Core.Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image[] _heartImages;
        private HealthComponent _health;

        private int _previousHealthValue;

        public void Construct(HealthComponent health)
        {
            _health = health;
            _previousHealthValue = health.Value;
            _health.Changed += UpdateImages;
        }

        private void UpdateImages(int value)
        {
            if (_previousHealthValue < value)
                _heartImages.FirstOrDefault(x => x.gameObject.activeInHierarchy == false)?.gameObject.SetActive(true);
            else
                _heartImages.FirstOrDefault(x => x.gameObject.activeInHierarchy)?.gameObject.SetActive(false);

            _previousHealthValue = value;
        }

        private void OnDisable()
        {
            _health.Changed -= UpdateImages;
        }
    }
}