using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _startSize = 10;

    private Queue<GameObject> _shelf = new Queue<GameObject>();

    private void Awake() {
        for (int i = 0; i < _startSize; i++) _shelf.Enqueue(CreateNew());
    }

    public GameObject Get(Vector3 position, Quaternion rotation) {
        GameObject obj = _shelf.Count > 0 ? _shelf.Dequeue() : CreateNew();

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj) {
        obj.SetActive(false);
        _shelf.Enqueue(obj);
    }

    private GameObject CreateNew() {
        GameObject obj = Instantiate(_prefab, transform);
        obj.SetActive(false);
        return obj;
    }
}