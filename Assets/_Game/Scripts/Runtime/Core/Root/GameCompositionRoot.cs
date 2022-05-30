using NavySpade.Core.Configs;
using NavySpade.Core.CrystalInfrastructure;
using NavySpade.Core.EnemyInfrastructure;
using NavySpade.Core.Health;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Managers;
using NavySpade.Core.PlayerInfrastructure;
using NavySpade.Core.Scores;
using UnityEngine;

namespace NavySpade.Core.Root
{
    public class GameCompositionRoot : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private GameObject _playerGameObject;
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private Transform _crystalContainer;
        [SerializeField] private Transform _walkableArea;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private CrystalView _crystalView;
        [SerializeField] private BestScoreView _bestScoreView;
        [SerializeField] private Camera _camera;

        private SaveSystem _saveSystem;
        private InitializeManager _initializeManager;
        private TickableManager _tickableManager;
        private DisposableManager _disposableManager;
        private CameraTracker _cameraTracker;

        private void Awake()
        {
            var player = new Player(_playerGameObject, _camera, _skinnedMeshRenderer, _playerConfig, this);

            _cameraTracker = new CameraTracker(_camera, _playerGameObject.transform, _gameConfig.CameraOffset);

            var enemySpawner = new EnemySpawner(_gameConfig.EnemyPrefab, _enemyContainer, this,
                _enemyConfig, _walkableArea, _gameConfig.EnemySpawnCount, _gameConfig.EnemySpawnInterval);

            var crystalSpawner = new CrystalSpawner(_gameConfig.CrystalPrefab, _crystalContainer, this, _walkableArea,
                _gameConfig.CrystalStartSpawnCount, _gameConfig.CrystalAdditionalSpawnCount,
                _gameConfig.CrystalAdditionalSpawnInterval);

            var score = new Score(_gameConfig.RandomScoreIncome);
            var bestScore = new BestScore(score);

            _scoreView.Construct(score);
            _bestScoreView.Construct(bestScore);
            _enemyView.Construct(enemySpawner);
            _crystalView.Construct(crystalSpawner);
            var scoreSystem = new ScoreSystem(player, score);

            _healthView.Construct(player.HealthComponent);
            var healthSystem = new HealthSystem(player);

            _saveSystem = new SaveSystem(bestScore);

            _initializeManager = new InitializeManager(enemySpawner,
                crystalSpawner,
                _scoreView,
                _bestScoreView,
                _enemyView,
                _crystalView,
                _saveSystem,
                healthSystem,
                scoreSystem,
                bestScore,
                player);

            _tickableManager = new TickableManager(player);

            _disposableManager = new DisposableManager(_saveSystem,
                healthSystem,
                scoreSystem,
                player);
        }

        private void Start() => _initializeManager.Initialize();

        private void Update() => _tickableManager.Tick();

        private void LateUpdate() => _cameraTracker.Tick();

        private void OnDestroy() => _disposableManager.Dispose();
    }
}