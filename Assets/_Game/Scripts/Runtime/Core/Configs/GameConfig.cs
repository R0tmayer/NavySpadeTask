
using UnityEngine;

namespace NavySpade.Core.Configs
{
    [CreateAssetMenu(menuName = "Configs/Game Config", fileName = "Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private Vector3 _cameraOffset;
        [Header("Enemy Spawner")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _enemySpawnCount;
        [SerializeField] private float _enemySpawnInterval;
        
        [Header("Crystal Spawner")]
        [SerializeField] private GameObject _crystalPrefab;
        [SerializeField] private int _crystalStartSpawnCount;
        [SerializeField] private int _crystalAdditionalSpawnCount;
        [SerializeField] private float _crystalAdditionalSpawnInterval;
        [SerializeField] private Vector2 _randomScoreIncome;

        public GameObject EnemyPrefab => _enemyPrefab;
        public GameObject CrystalPrefab => _crystalPrefab;
        public int EnemySpawnCount => _enemySpawnCount;
        public Vector3 CameraOffset => _cameraOffset;
        public Vector2 RandomScoreIncome => _randomScoreIncome;

        public float EnemySpawnInterval => _enemySpawnInterval;

        public int CrystalStartSpawnCount => _crystalStartSpawnCount;

        public int CrystalAdditionalSpawnCount => _crystalAdditionalSpawnCount;

        public float CrystalAdditionalSpawnInterval => _crystalAdditionalSpawnInterval;
    }
}