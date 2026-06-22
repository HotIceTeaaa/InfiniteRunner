using UnityEngine;

namespace InfiniteRunner
{
    public class ObsticleSpawner : MonoBehaviour
    {
        [Header("For Summoning Obsticles")]
        [SerializeField] private GameObject _obsticlePrefab;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _spawnThreshold;

        private float _currentTimer;
        private Vector3[] _spawnPositions;

        void Start()
        {
            // posisisnya:
            // 0 1 2 
            // 3 4 5
            _spawnPositions[0] = new Vector3(-2.27999997f,4.09000015f,33.8600006f);
            _spawnPositions[1] = new Vector3(0.115999997f,4.09000015f,33.8600006f);
            _spawnPositions[2] = new Vector3(2.56900001f,4.09000015f,33.8600006f);
            _spawnPositions[3] = new Vector3(-2.27999997f,2.33999991f,33.8600006f);
            _spawnPositions[4] = new Vector3(0.115999997f,2.33999991f,33.8600006f);
            _spawnPositions[5] = new Vector3(2.56900001f,2.33999991f,33.8600006f);
        }

        void Update()
        {
            DecrementTimer();

            if( _currentTimer < 0) {
                SpawnObsticles();
                ResetTimer();
            }
        }

        private void SpawnObsticles() {
            //bikin array spawn probabilities untuk setiap posisi
            float[] spawnProbabilities = new float[6];

            for(int i = 0; i < spawnProbabilities.Length; i++)
            {
                spawnProbabilities[i] = Random.value;
            }

            //make sure obsticlesnya bs dilewatin
            //kalo posisi 0 1 2 diatas spawn threshold, ilangin salah satu 
            if (spawnProbabilities[0] > _spawnThreshold &&
                spawnProbabilities[1] > _spawnThreshold &&
                spawnProbabilities[2] > _spawnThreshold)
            {
                int i = Random.Range(0, 3);
                spawnProbabilities[i] = 0f;
            }

            //mulai instantiate obsticlesnya
            for(int i = 0; i < spawnProbabilities.Length; i++)
            {
                if(spawnProbabilities[i] > _spawnThreshold)
                {
                    Debug.Log(_obsticlePrefab.name);
                    Instantiate(_obsticlePrefab, _spawnPositions[i], Quaternion.identity);
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
