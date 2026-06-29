using System.Collections;
using UnityEngine;

public class PoolDemo : MonoBehaviour {
    [SerializeField] private ObjectPoolManager _pool;
    [SerializeField] private float _spawnEvery = 0.5f;
    [SerializeField] private float _lifeTime = 2f;

    private float timer;

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= _spawnEvery) {
            timer = 0f;
            SpawnOne();
        }
    }

    private void SpawnOne() {
        GameObject obj = _pool.Get(transform.position, Quaternion.identity);

        StartCoroutine(ReturnAfter(obj, _lifeTime));
    }

    private IEnumerator ReturnAfter(GameObject obj, float delay) {
        yield return new WaitForSeconds(delay);
        if (obj.activeSelf) _pool.Return(obj);
    }
}