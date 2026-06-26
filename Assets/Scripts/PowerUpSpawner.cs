using UnityEngine;

namespace InfiniteRunner {
    public class PowerUpSpawner : MonoBehaviour {
        [Header("For Summoning Obsticles")]
        [SerializeField] private GameObject _shieldPrefab;
        [SerializeField] private GameObject _scoreMultiplierPrefab;
        [SerializeField] private GameObject[] _locations;

        [SerializeField] private float _spawnRate;
        [SerializeField] private float _spawnThreshold;

        private float _currentTimer;

        void Update() {
            DecrementTimer();

            if (_currentTimer < 0) {
                SpawnPowerUp();
                ResetTimer();
            }
        }

        private void SpawnPowerUp() {
            for (int i = 0; i < _locations.Length; i++) {
                float spawnProbability = Random.value;

                if (spawnProbability > _spawnThreshold) {
                    int whichPowerup = Random.Range(0, 1);
                    int whichLanes = Random.Range(0, _locations.Length);

                    GameObject powerUp = null;

                    switch (whichPowerup) {
                        case 0:
                            powerUp = Instantiate(_shieldPrefab, _locations[whichLanes].transform.position, Quaternion.identity);
                            break;
                        case 1:
                            powerUp = Instantiate(_scoreMultiplierPrefab, _locations[whichLanes].transform.position, Quaternion.identity);
                            break;
                    }
                    
                    Destroy(powerUp, 10);
                }
            }
        }

        private void ResetTimer() {
            _currentTimer = _spawnRate;
        }

        private void DecrementTimer() {
            _currentTimer -= Time.deltaTime;
        }
    }

}
