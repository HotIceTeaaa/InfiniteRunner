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

            //pengennya di destroy on load
            //krn pas GameOver() dipanggil, bakal reload scene, _isGameOver bakal di set false lagi
            //DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            _isGameOver = false;
            BGMManager.Instance.PlayMainMenuBGM();
            MainMenuUIManager.Instance.UpdateCoinCount();
        }

        public void GameOver() {
            // _isGameOver g akan pernah true sebelom method ini dipanggil
            // ini buat mastiin doang
            if (_isGameOver) {
                return;
            }

            _isGameOver = true;
            SaveCollectedCoinsThisRound();
            SceneHandler.Instance.LoadSceneByIndex(0);
            BGMManager.Instance.PlayMainMenuBGM();
            MainMenuUIManager.Instance.UpdateCoinCount();
        }

        private void SaveCollectedCoinsThisRound()
        {
            int coins = PlayerPreferences.Instance.getInt("coins", 0);
            coins += _coinsCollectedThisRound;
            PlayerPreferences.Instance.saveInt("coins", coins);
        }

        private void CoinCollected() {
            _coinsCollectedThisRound++;
        }

    }
}

