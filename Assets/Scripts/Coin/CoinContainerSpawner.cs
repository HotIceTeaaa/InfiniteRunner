using InifiniteRunner;
using System.Collections;
using UnityEngine;

namespace InfiniteRunner {
    public class CoinSpawner : MonoBehaviour {
        [Header("For Summoning Coin Containers")]
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
                    GameObject coinContainer = PoolManager.Instance.GetAndSetPositionRotation(PoolType.CoinContainer, _coinContainerSpawnTransforms[i].position, Quaternion.identity);
                    
                    CoinContainer coinContainerScript = coinContainer.GetComponent<CoinContainer>();
                    coinContainerScript.Initialize();

                    StartCoroutine(ReturnAfter(PoolType.CoinContainer, coinContainer, _lifeTime));
                }
            }
        }

        private IEnumerator ReturnAfter(PoolType type, GameObject obj, float lifespan) {
            yield return new WaitForSeconds(lifespan);

            if (obj.activeSelf) {
                PoolManager.Instance.Return(type, obj);
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
