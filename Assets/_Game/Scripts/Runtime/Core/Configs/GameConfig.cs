
using UnityEngine;

namespace NavySpade.Core.Configs
{
    [CreateAssetMenu(menuName = "Configs/Game Config", fileName = "Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [Header("Enemy Spawner")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _enemyCount;
        
        [SerializeField] private GameObject _crystalPrefab;

        public GameObject EnemyPrefab => _enemyPrefab;
        public GameObject CrystalPrefab => _crystalPrefab;

        public int EnemyCount => _enemyCount;
    }
}