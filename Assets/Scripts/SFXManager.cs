using UnityEngine;

namespace InfiniteRunner
{
    public class SFXManager : MonoBehaviour {
        public static SFXManager Instance { get; private set; }
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _loopedAudioSource;

        [Header("CoinCollectSFX")]
        [SerializeField] private AudioClip _coinCollectSFX;
        [SerializeField] private float _pitchMultiplier;
        [SerializeField] private float _timeElapsedThreshold;

        [Header("Player SFX")]
        [SerializeField] private AudioClip _jumpSFX;
        [SerializeField] private AudioClip _slideSFX;
        //[SerializeField] private AudioClip _runningSFX; running clip langsung di reference di looped audio source
        [SerializeField] private AudioClip _deathSFX;
        [SerializeField] private AudioClip _changeLanesSFX;

        [Header("Powerup SFX")]
        [SerializeField] private AudioClip _boughtUpgradesSFX;
        [SerializeField] private AudioClip _buyErrorSFX;
        [SerializeField] private AudioClip _powerupCollectSFX;
        [SerializeField] private AudioClip _powerupDepletedSFX;

        [Header("UI SFX")]
        [SerializeField] private AudioClip _buttonClickHoverSFX;
        

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

        //player sfx
        public void PlayRunSFX(){_loopedAudioSource.mute = false;}
        public void MuteRunSFX(){_loopedAudioSource.mute = true;}
        public void PlayJumpSFX(){_audioSource.PlayOneShot(_jumpSFX);}
        public void PlaySlideSFX(){/*g nemu sfx slide yg bagus jadi di remove*/}
        public void PlayDeathSFX(){_audioSource.PlayOneShot(_deathSFX);}
        public void PlayChangeLanesSFX(){_audioSource.PlayOneShot(_changeLanesSFX);}

        //powerup sfx
        public void PlayBoughtUpgradeSFX(){_audioSource.PlayOneShot(_boughtUpgradesSFX);}
        public void PlayBuyErrorSFX(){_audioSource.PlayOneShot(_buyErrorSFX);}
        public void PlayPowerUpCollectSFX(){_audioSource.PlayOneShot(_powerupCollectSFX);}
        public void PlayPowerupDepletedSFX(){_audioSource.PlayOneShot(_powerupDepletedSFX);}

        //ui sfx
        public void PlayClickHoverSFX(){_audioSource.PlayOneShot(_buttonClickHoverSFX);}
        
    }

}
