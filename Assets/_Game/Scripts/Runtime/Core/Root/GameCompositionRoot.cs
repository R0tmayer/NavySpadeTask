using NavySpade.Core.Configs;
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
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private Transform _walkableArea;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private BestScoreView _bestScoreView;
        [SerializeField] private Camera _camera;

        private SaveSystem _saveSystem;
        private InitializeManager _initializeManager;
        private TickableManager _tickableManager;
        private DisposableManager _disposableManager;

        private void Awake()
        {
            var player = new Player(_playerGameObject, _camera, _playerConfig, this);
            
            var enemySpawner = new EnemySpawner(_gameConfig.EnemyPrefab, _enemyContainer, this, 
                _enemyConfig, _walkableArea, _gameConfig.EnemyCount);

            var score = new Score();
            var bestScore = new BestScore(score);

            _scoreView.Construct(score);
            _bestScoreView.Construct(bestScore);
            var scoreSystem = new ScoreSystem(player, score);
            
            _healthView.Construct(player.HealthComponent);
            var healthSystem = new HealthSystem(player);

            _saveSystem = new SaveSystem(bestScore);

            _initializeManager = new InitializeManager(enemySpawner,
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

        private void OnDestroy() => _disposableManager.Dispose();
    }
}