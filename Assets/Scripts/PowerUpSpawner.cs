using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace InfiniteRunner {
    public class PowerUpSpawner : MonoBehaviour {
        [Header("For Summoning Obsticles")]
        [SerializeField] private GameObject _shieldPrefab;
        [SerializeField] private GameObject _scoreMultiplierPrefab;
        [SerializeField] private GameObject[] _locations;

        public static event Action OnShieldLoss;
        public static event Action OnScoreMultiplierLoss;

        void Update() {
            HandleShieldSpawn();
            HandleShieldDuration();
            
            HandleScoreMultiplierSpawn();
            HandleScoreMultiplierDuration();
        }

        private void HandleShieldSpawn()
        {
            PlayerStates.Instance._shieldSpawnAttemptCooldown -= Time.deltaTime;

            if(PlayerStates.Instance._shieldSpawnAttemptCooldown <= 0f)
            {
                float spawnProbability = Random.value;
                if (spawnProbability < PlayerStates.Instance._shieldSpawnThreshold) 
                {
                    int whichLanes = Random.Range(0, _locations.Length);

                    GameObject powerUp = Instantiate(_shieldPrefab, _locations[whichLanes].transform.position, Quaternion.identity);
                    Destroy(powerUp, 10);
                }
                
                //Reset timer
                PlayerStates.Instance._shieldSpawnAttemptCooldown = PlayerStates.Instance._shieldSpawnAttemptRate;
            }
        }

        private void HandleShieldDuration()
        {
            if (PlayerStates.Instance._isShielded) {
                if (PlayerStates.Instance._shieldDurationLeft < 0f) {
                    PlayerStates.Instance._isShielded = false;
                    PlayerStates.Instance._shieldDurationLeft = PlayerStates.Instance._shieldDuration;
                } else {
                    PlayerStates.Instance._shieldDurationLeft -= Time.deltaTime;
                }
            }
        }

        private void HandleScoreMultiplierSpawn()
        {
            PlayerStates.Instance._scoreMultiplierSpawnAttemptCooldown -= Time.deltaTime;

            if(PlayerStates.Instance._scoreMultiplierSpawnAttemptCooldown <= 0f)
            {
                float spawnProbability = Random.value;
                if (spawnProbability < PlayerStates.Instance._scoreMultiplierSpawnThreshold) 
                {
                    int whichLanes = Random.Range(0, _locations.Length);

                    GameObject powerUp = Instantiate(_scoreMultiplierPrefab, _locations[whichLanes].transform.position, Quaternion.identity);
                    Destroy(powerUp, 10);
                }
                
                //Reset timer
                PlayerStates.Instance._scoreMultiplierSpawnAttemptCooldown = PlayerStates.Instance._scoreMultiplierSpawnAttemptRate;
            }
        }

        private void HandleScoreMultiplierDuration()
        {
            if (PlayerStates.Instance._isScoreMultiplied) {
                if (PlayerStates.Instance._scoreMultiplierDurationLeft < 0f) {
                    PlayerStates.Instance._isScoreMultiplied = false;
                    PlayerStates.Instance._scoreMultiplierDurationLeft = PlayerStates.Instance._scoreMultiplierDuration;
                } else {
                    PlayerStates.Instance._scoreMultiplierDurationLeft -= Time.deltaTime;
                }
            }
        }
    }

}
