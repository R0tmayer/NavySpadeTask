using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NavySpade.Core.Managers
{
    public class Mediator : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private GameObject _endGamePanel;

        public event Action GameStarted;

        private void Start()
        {
            _endGamePanel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(StartGame);
            _restartButton.onClick.AddListener(ReloadScene);
        }        
        
        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(StartGame);
            _restartButton.onClick.RemoveListener(ReloadScene);
        }

        public void ShowEndGamePanel()
        {
            Time.timeScale = 0;
            _endGamePanel.SetActive(true);
        }

        private void StartGame()
        {
            _playButton.gameObject.SetActive(false);
            GameStarted?.Invoke();
        }

        private void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}