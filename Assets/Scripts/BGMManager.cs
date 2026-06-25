using UnityEngine;

namespace InfiniteRunner
{
    public class BGMManager : MonoBehaviour
    {
        public static BGMManager Instance { get; private set; }
        [SerializeField] private AudioSource _audioSource;

        private void Awake() {
            // Enforce the Singleton pattern
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayMainMenuBGM()
        {
            _audioSource.volume = 0.1f;
            _audioSource.Play();
        }

        public void PlayGameplayBGM()
        {
            _audioSource.volume = 0.8f;
            _audioSource.Play();
        }
    }

}
