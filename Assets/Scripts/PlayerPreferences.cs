using UnityEngine;

namespace InfiniteRunner
{
    public class PlayerPreferences : MonoBehaviour {
        public static PlayerPreferences Instance { get; private set; }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        public void saveFloat(string key, float value) {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public float getFloat(string key, float defaultValue) {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public void saveInt(string key, int value) {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public int getInt(string key, int defaultValue) {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        [ContextMenu("Clear All Keys")]
        public void clearAllKeys() {
            PlayerPrefs.DeleteAll();
        }

        [ContextMenu("Print Coins")]
        public void PrintCoins() {
            int coins = PlayerPrefs.GetInt("coins", -999);
            Debug.Log(coins);
        }
    }
}