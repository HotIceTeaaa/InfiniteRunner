using UnityEngine;

namespace InfiniteRunner {
    public class PlayerStates : MonoBehaviour {
        public static PlayerStates Instance { get; private set; }

        public bool _isJumping = false;
        public bool _isSliding = false;

        public bool _isGameOver = false;
        public bool _isGameStart = false;

        public bool _isShielded = false;
        public float _shieldDurationLeft = 0f;

        public bool _isScoreMultiplied = false;
        public float _scoreMultiplierDurationLeft = 0f;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        private void Update() {
            if (_isShielded) {
                if (_shieldDurationLeft < 0f) {
                    _isShielded = false;
                } else {
                    _shieldDurationLeft -= Time.deltaTime;
                }
            }

            if (_isScoreMultiplied) {
                if (_scoreMultiplierDurationLeft < 0f) {
                    _isScoreMultiplied = false;
                } else {
                    _scoreMultiplierDurationLeft -= Time.deltaTime;
                }
            }
        }
    }

}
