using NavySpade.Core.EnemyInfrastructure;
using NavySpade.Core.Interfaces;
using TMPro;
using UnityEngine;

namespace NavySpade.Core.CrystalInfrastructure
{
    public class CrystalView : MonoBehaviour, IInitializable
    {
        [SerializeField] private TMP_Text _text;
        private CrystalSpawner _crystalSpawner;

        public void Construct(CrystalSpawner crystalSpawner)
        {
            _crystalSpawner = crystalSpawner;
        }

        public void Initialize()
        {
            _crystalSpawner.Spawned += UpdateText;
            _crystalSpawner.CrystalCollected += UpdateText;
        }

        private void UpdateText(int value)
        {
            _text.SetText($"{value}");
        }
        
        private void OnDisable()
        {
            _crystalSpawner.Spawned -= UpdateText;
            _crystalSpawner.CrystalCollected -= UpdateText;

        }
    }
}