using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfiniteRunner {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        [Header("Score Related")]
        [SerializeField] private float _score = 0;
        [SerializeField] private float _scoreIncrements = 100f;
        [SerializeField] private float _scoreMultiplier = 1f;

        [Header("Speed Related")]
        [SerializeField] private float _speed = 8f;
        [SerializeField] private float _increaseSpeedBy = 0.1f;
        [SerializeField] private int _increaseSpeedCount = 0;
        [SerializeField] private int _increaseSpeedEveryScore = 10000;

        public float Speed => _speed;
        public float Score => _score;

        public int coinsCollectedThisRound = 0;

        private float _highScore = -9999;


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

            //DontDestroyOnLoad(gameObject);
            GameStart();
        }

        void Start()
        {
            _highScore = PlayerPreferences.Instance.getFloat("highScore", 0f);
        }

        private void FixedUpdate() {
            HandleScoreIncrease();
            HandleSpeedIncrease();
        }

        public void GameStart() {
            PlayerStates.Instance._isGameOver = false;
            PlayerStates.Instance._isGameStart = true;

            BGMManager.Instance.PlayGameplayBGM();
        }

        public void GameOver() {
            // _isGameOver g akan pernah true sebelom method ini dipanggil
            // ini buat mastiin doang
            if (PlayerStates.Instance._isGameOver) {
                return;
            }

            PlayerStates.Instance._isGameOver = true;
            PlayerStates.Instance._isGameStart = false;

            PlayerStates.Instance.ResetPlayerStates();

            SaveCollectedCoinsThisRound();
            UpdateHighScore();

            ToMainMenu();
        }

        public void ToMainMenu() {
            PlayerStates.Instance._isGameOver = false;
            PlayerStates.Instance._isGameStart = false;

            SceneHandler.Instance.LoadSceneByIndex(0);
            BGMManager.Instance.PlayMainMenuBGM();
        }

        private void SaveCollectedCoinsThisRound()
        {
            int coins = PlayerPreferences.Instance.getInt("coins", 0);
            coins += coinsCollectedThisRound;
            PlayerPreferences.Instance.saveInt("coins", coins);
        }
        private void UpdateHighScore() {
            if (_score > _highScore) {
                _highScore = _score;
                PlayerPreferences.Instance.saveFloat("highScore", _highScore);
            }
        }
        private void CoinCollected() {
            coinsCollectedThisRound++;
        }

        private void HandleSpeedIncrease() {
            int result = Mathf.FloorToInt(_score / _increaseSpeedEveryScore);
            
            if(result > _increaseSpeedCount) {
                _increaseSpeedCount = result;
                _speed += _increaseSpeedBy;
            }
        }

        private void HandleScoreIncrease() {
            if (PlayerStates.Instance._isGameStart) {
                if (PlayerStates.Instance._isScoreMultiplied) {
                    _score += _scoreIncrements * _scoreMultiplier;
                } else {
                    _score += _scoreIncrements;
                }
            }
        }
    }
}

