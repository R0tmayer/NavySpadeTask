using System;
using NavySpade.Core.Interfaces;
using TMPro;
using UnityEngine;

namespace NavySpade.Core.EnemyInfrastructure
{
    public class EnemyView : MonoBehaviour, IInitializable
    {
        [SerializeField] private TMP_Text _text;
        private EnemySpawner _enemySpawner;

        public void Construct(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public void Initialize()
        {
            _enemySpawner.Spawned += UpdateText;
        }

        private void UpdateText(int value)
        {
            _text.SetText($"{value}");
        }

        private void OnDisable()
        {
            _enemySpawner.Spawned -= UpdateText;
        }
    }
}