using UnityEngine;

namespace InfiniteRunner
{
    public class ObsticleSpawner : MonoBehaviour
    {
        [Header("For Summoning Obsticles")]
        [SerializeField] private GameObject _floatingObsticlePrefab;
        [SerializeField] private GameObject _shortObsticlePrefab;
        [SerializeField] private GameObject _tallObsticlePrefab;

        [SerializeField] private GameObject[] _locations;

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
            //kalo semua posisi diatas spawn threshold, ilangin salah satu 
            if (spawnProbabilities[0] > _spawnThreshold &&
                spawnProbabilities[1] > _spawnThreshold &&
                spawnProbabilities[2] > _spawnThreshold &&
                spawnProbabilities[3] > _spawnThreshold &&
                spawnProbabilities[4] > _spawnThreshold &&
                spawnProbabilities[5] > _spawnThreshold)
            {
                int i = Random.Range(0, spawnProbabilities.Length);
                spawnProbabilities[i] = 0f;
            }

            //mulai instantiate obsticlesnya
            //index mulai dari atas ke bawah
            for(int i = spawnProbabilities.Length - 1; i >= 0; i--)
            {
                if (spawnProbabilities[i] > _spawnThreshold) {
                    //handle obsticle yang bottom sama top halfnya bisa jadi obsticle
                    if (i >= 3 && spawnProbabilities[i - 3] > _spawnThreshold) {
                        GameObject obs = Instantiate(_tallObsticlePrefab, _spawnPositions[i], Quaternion.identity);
                        spawnProbabilities[i - 3] = 0;
                        Destroy(obs, 10);

                    //handle obsticle yang bottom halfnya doang bisa jadi obsticle
                    } else if (i >= 3) {
                        GameObject obs = Instantiate(_shortObsticlePrefab, _spawnPositions[i], Quaternion.identity);
                        Destroy(obs, 10);

                    //handle obsticle yang top halfnya doang bisa jadi obsticle
                    } else {
                        GameObject obs = Instantiate(_floatingObsticlePrefab, _spawnPositions[i], Quaternion.identity);
                        Destroy(obs, 10);
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
            _spawnPositions = new Vector3[_locations.Length];

            for (int i = 0; i < _locations.Length; i++) {
                _spawnPositions[i] = _locations[i].transform.position;
            }
        }
    }

}
