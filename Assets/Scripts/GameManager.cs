using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfiniteRunner {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        [SerializeField] private bool _isGameOver = false;
        [SerializeField] private float _speed = 0.5f;

        //[Header("Script Lain")]
        //[SerializeField] private ;

        public float Speed => _speed;
        public bool IsGameOver => _isGameOver;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update() {
            if (!_isGameOver) {
                return;
            }
        }

        public void GameOver() {
            if (!_isGameOver) {
                return;
            }

            _isGameOver = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

    }
}

