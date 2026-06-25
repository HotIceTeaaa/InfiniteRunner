using UnityEngine;

namespace InfiniteRunner
{
    public class SFXManager : MonoBehaviour {
        public static SFXManager Instance { get; private set; }
        
        [SerializeField] private AudioSource _audioSource;

        [Header("CoinCollectSFX")]
        [SerializeField] private AudioClip _coinCollectSFX;
        [SerializeField] private float _pitchMultiplier;
        [SerializeField] private float _timeElapsedThreshold;

        private float _lastTimeCoinCollected = -999999f;

        private void OnEnable() {
            Coins.OnCollect += PlayCoinCollectSFX;
        }

        private void OnDisable() {
            Coins.OnCollect -= PlayCoinCollectSFX;
        }

        private void Awake() {
            // Enforce the Singleton pattern
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayCoinCollectSFX() {
            float timeElapsed = Time.time - _lastTimeCoinCollected;
            _lastTimeCoinCollected = Time.time;

            float pitch = _audioSource.pitch;

            if(timeElapsed < _timeElapsedThreshold){
                pitch *= _pitchMultiplier;
            }
            else
            {
                pitch = 1;
            }

            _audioSource.pitch = pitch;
            _audioSource.PlayOneShot(_coinCollectSFX);
        }
    }

}
