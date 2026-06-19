using System.Threading;
using UnityEngine;

public class ObsticleSpawner : MonoBehaviour
{
    [Header("For Summoning Obsticles")]
    [SerializeField] private GameObject _obsticlePrefab;
    [SerializeField] private float _spawnRate;

    private float _currentTimer;


    void Update()
    {
        DecrementTimer();

        if( _currentTimer < 0) {
            SpawnObsticles();
            ResetTimer();
        }
    }

    private void SpawnObsticles() {

    }

    private void ResetTimer() {
        _currentTimer = _spawnRate;
    }

    private void DecrementTimer() {
        _currentTimer -= Time.deltaTime;
    }
}
