using UnityEngine;

namespace InfiniteRunner
{
    public class ObsticleSpawner : MonoBehaviour
    {
        [Header("For Summoning Obsticles")]
        [SerializeField] private GameObject _obsticlePrefab;
        [SerializeField] private GameObject[] _obsticles;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _spawnThreshold;

        private float _currentTimer;
        private Vector3[] _spawnPositions;

        void Start()
        {
            SetSpawnPositions();
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
                    GameObject obsticle = Instantiate(_obsticlePrefab, _spawnPositions[i], Quaternion.identity);
                    Destroy(obsticle, 10);

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
            _spawnPositions = new Vector3[_obsticles.Length];

            for (int i = 0; i < _obsticles.Length; i++) {
                _spawnPositions[i] = _obsticles[i].transform.position;
            }
        }
    }

}
