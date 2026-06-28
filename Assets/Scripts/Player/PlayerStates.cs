using UnityEngine;

namespace InfiniteRunner {
    public class PlayerStates : MonoBehaviour {
        public static PlayerStates Instance { get; private set; }

        [Header("Player States")]
        public bool _isJumping = false;
        public bool _isSliding = false;

        [Header("Game States")]
        public bool _isGameOver = false;
        public bool _isGameStart = false;

        [Header("Shield Powerup Related")]
        public bool _isShielded = false;
        public float _shieldDurationLeft = 0f;
        public float _shieldDuration;
        public float _shieldSpawnThreshold;
        public float _shieldSpawnAttemptRate;
        public float _shieldSpawnAttemptCooldown;

        [Header("2x Score Multiplier Powerup Related")]
        public bool _isScoreMultiplied = false;
        public float _scoreMultiplierDurationLeft = 0f;
        public float _scoreMultiplierDuration;
        public float _scoreMultiplierSpawnThreshold;
        public float _scoreMultiplierSpawnAttemptRate;
        public float _scoreMultiplierSpawnAttemptCooldown;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            InitPowerUpInfo();
        }

        public void InitPowerUpInfo()
        {
            //init shield powerup
            int level = PlayerPreferences.Instance.getInt("shieldLevel", 1);

            switch (level)
            {
                case 1:
                    _shieldDuration = 10;
                    _shieldSpawnThreshold = 0.1f;
                    _shieldSpawnAttemptRate = 10;
                    break;
                case 2:
                    _shieldDuration = 20;
                    _shieldSpawnThreshold = 0.2f;
                    _shieldSpawnAttemptRate = 9;
                    break;
                case 3:
                    _shieldDuration = 30;
                    _shieldSpawnThreshold = 0.3f;
                    _shieldSpawnAttemptRate = 8;
                    break;
            }

            //init score multiplier
            level = PlayerPreferences.Instance.getInt("scoreMultiplierLevel", 1);

            switch (level)
            {
                case 1:
                    _scoreMultiplierDuration = 10;
                    _scoreMultiplierSpawnThreshold = 0.1f;
                    _scoreMultiplierSpawnAttemptRate = 10;
                    break;
                case 2:
                    _scoreMultiplierDuration = 20;
                    _scoreMultiplierSpawnThreshold = 0.2f;
                    _scoreMultiplierSpawnAttemptRate = 9;
                    break;
                case 3:
                    _scoreMultiplierDuration = 30;
                    _scoreMultiplierSpawnThreshold = 0.3f;
                    _scoreMultiplierSpawnAttemptRate = 8;
                    break;
            }
        }
    }
}

