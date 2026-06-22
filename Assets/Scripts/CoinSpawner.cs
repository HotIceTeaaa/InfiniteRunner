using UnityEngine;

namespace InfiniteRunner {
    public class CoinSpawner : MonoBehaviour {
        [Header("For Summoning Coins")]
        [SerializeField] private GameObject _coinRowPrefab;
        [SerializeField] private GameObject _coinArchPrefab;
        [SerializeField] private GameObject[] _coins;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _spawnThreshold;

        private float _currentTimer;
        private Vector3[] _spawnPositions;

        void Start() {
            SetSpawnPositions();
        }

        void Update() {
            DecrementTimer();

            if (_currentTimer < 0) {
                SpawnCoins();
                ResetTimer();
            }
        }

        private void SpawnCoins() {
            //mulai instantiate obsticlesnya
            for (int i = 0; i < 3; i++) {
                float spawnProbabilities = Random.value;

                if (spawnProbabilities > _spawnThreshold) {
                    int coinType = Random.Range(0, 2);

                    switch (coinType) {
                        case 0:
                            GameObject coinRow = Instantiate(_coinRowPrefab, _spawnPositions[i], Quaternion.identity);
                            Destroy(coinRow, 10);
                            break;
                        case 1:
                            GameObject coinArch = Instantiate(_coinArchPrefab, _spawnPositions[i], Quaternion.identity);
                            Destroy(coinArch, 10);
                            break;
                    }
                }
            }
        }

        private void ResetTimer() {
            _currentTimer = _spawnRate;
        }

        private void DecrementTimer() {
            _currentTimer -= Time.deltaTime;
        }

        private void SetSpawnPositions() {
            _spawnPositions = new Vector3[_coins.Length];

            for (int i = 0; i < _coins.Length; i++) {
                _spawnPositions[i] = _coins[i].transform.position;
            }
        }
    }

}
