using System;
using System.Collections;
using System.Collections.Generic;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Old;
using NavySpade.Core.Scores;
using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Core.Root
{
    public class GameCompositionRoot : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private GameObject _playerGameObject;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private BestScoreView _bestScoreView;
        [SerializeField] private Camera _camera;

        private SaveSystem _saveSystem;
        private InitializeManager _initializeManager;
        private TickableManager _tickableManager;
        private DisposableManager _disposableManager;

        private void Awake()
        {
            var player = new Player(_playerGameObject, _camera, _playerConfig);

            var enemyFactory = new EnemyFactory(_gameConfig.EnemyPrefab, _enemyContainer);
            var enemySpawner = new EnemySpawner(enemyFactory, _gameConfig.WalkableCollider, _gameConfig.EnemyCount);


            var score = new Score();
            var bestScore = new BestScore(score);

            _scoreView.Construct(score);
            _bestScoreView.Construct(bestScore);


            var scoreSystem = new ScoreSystem(player, score);
            _saveSystem = new SaveSystem(bestScore);

            _initializeManager = new InitializeManager(enemySpawner,
                _saveSystem,
                scoreSystem,
                bestScore,
                player);

            _tickableManager = new TickableManager(player);

            _disposableManager = new DisposableManager(_saveSystem,
                scoreSystem,
                player);
        }

        private void Start() => _initializeManager.Initialize();

        private void Update() => _tickableManager.Tick();

        private void OnDestroy() => _disposableManager.Dispose();
    }
}