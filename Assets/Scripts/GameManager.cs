using UnityEngine;

namespace InfiniteRunner {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }
        public bool IsGameOver = false;
        public float Speed = 0.5f;
        
        private void Awake() {
            // Enforce the Singleton pattern
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

