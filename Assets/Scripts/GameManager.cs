using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfiniteRunner {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        [SerializeField] private bool _isGameOver = false;
        [SerializeField] private float _speed = 0.5f;
        [SerializeField] private int _coinsCollectedThisRound = 0;

        public float Speed => _speed;
        public bool IsGameOver => _isGameOver;

        private void OnEnable() {
            Coins.OnCollect += CoinCollected;
        }

        private void OnDisable() {
            Coins.OnCollect -= CoinCollected;
        }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            _isGameOver = false;
        }

        public void GameOver() {
            if (_isGameOver) {
                return;
            }

            _isGameOver = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        private void CoinCollected() {
            _coinsCollectedThisRound++;
        }

    }
}

