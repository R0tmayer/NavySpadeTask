using System;
using TMPro;
using UnityEngine;

namespace NavySpade.Core.FPSCounter
{
    public class FPSDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private int _lastFrameIndex;
        private float[] _frameDeltaTimeArray;

        private void Awake()
        {
            _frameDeltaTimeArray = new float[50];
        }

        private void Update()
        {
            _frameDeltaTimeArray[_lastFrameIndex] = Time.unscaledDeltaTime;
            _lastFrameIndex = (_lastFrameIndex + 1) % _frameDeltaTimeArray.Length;
            _text.SetText($"{Mathf.RoundToInt(CalculateFPS())}");
        }

        private float CalculateFPS()
        {
            float total = 0f;

            foreach (var deltaTime in _frameDeltaTimeArray)
            {
                total += deltaTime;
            }

            return _frameDeltaTimeArray.Length / total;
        }
    }
}