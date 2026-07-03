using InifiniteRunner;
using System.Collections;
using UnityEngine;

namespace InfiniteRunner {
    public class CoinSpawner : MonoBehaviour {
        [Header("For Summoning Coin Containers")]
        [SerializeField] private GameObject _coinContainerPrefab;
        [SerializeField] private Transform[] _coinContainerSpawnTransforms;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _spawnThreshold;
        [SerializeField] private float _lifeTime;
        
        private float _currentTimer;

        void Update() {
            DecrementTimer();

            if (_currentTimer < 0) {
                SpawnCoinContainers();
                ResetTimer();
            }
        }

        private void SpawnCoinContainers() {
            //mulai instantiate obsticlesnya
            for (int i = 0; i < 3; i++) {
                float spawnProbabilities = Random.value;

                if (spawnProbabilities < _spawnThreshold) {
                    GameObject coinContainer = Instantiate(_coinContainerPrefab, _coinContainerSpawnTransforms[i].position, Quaternion.identity);

                    Destroy(coinContainer, _lifeTime);
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
