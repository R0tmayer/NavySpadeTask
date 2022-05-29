
using UnityEngine;

namespace NavySpade.Core.Configs
{
    [CreateAssetMenu(menuName = "Configs/Game Config", fileName = "Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private Transform _walkableCollider;
        [Header("Enemy Spawner")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _enemyCount;
        
        [SerializeField] private GameObject _crystalPrefab;

        public GameObject EnemyPrefab => _enemyPrefab;
        public Transform WalkableCollider => _walkableCollider;
        public GameObject CrystalPrefab => _crystalPrefab;

        public int EnemyCount => _enemyCount;
    }
}