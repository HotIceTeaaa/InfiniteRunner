using InifiniteRunner;
using System.Collections;
using UnityEngine;

namespace InfiniteRunner {
    public class CoinSpawner : MonoBehaviour {
        [Header("For Summoning Coins")]
        [SerializeField] private CoinInitializer _coinInitializerScript;
        [SerializeField] private GameObject[] _coinGameObjects;

        [SerializeField] private float _spawnRate;
        [SerializeField] private float _spawnThreshold;
        [SerializeField] private float _lifeTime = 7f;
        
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

                if (spawnProbabilities < _spawnThreshold) {
                    GameObject coinContainer = PoolManager.Instance.GetAndSetPositionRotation(PoolType.CoinContainer, _spawnPositions[i], Quaternion.identity);

                    int coinType = Random.Range(0, 2);
                    int N = (coinType == 0) ? 5 : 7;

                    StartCoroutine(ReturnAfter(PoolType.CoinContainer, N, coinContainer, _lifeTime));

                    _coinInitializerScript.InitCoins(coinType, coinContainer);
                }
            }
        }

        private IEnumerator ReturnAfter(PoolType type, int N, GameObject obj, float lifespan) {
            yield return new WaitForSeconds(lifespan);

            if (obj.activeSelf) {
                PoolManager.Instance.Return(type, obj);
            }

            _coinInitializerScript.ReturnFirstNCoins(N);
        }

        private void ResetTimer() {
            _currentTimer = _spawnRate;
        }

        private void DecrementTimer() {
            _currentTimer -= Time.deltaTime;
        }

        private void SetSpawnPositions() {
            _spawnPositions = new Vector3[_coinGameObjects.Length];

            for (int i = 0; i < _coinGameObjects.Length; i++) {
                _spawnPositions[i] = _coinGameObjects[i].transform.position;
            }
        }
    }

}
