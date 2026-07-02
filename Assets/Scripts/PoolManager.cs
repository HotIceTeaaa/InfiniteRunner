using System.Collections.Generic;
using UnityEngine;

public enum PoolType {
    Coin, 
    CoinContainer,
}

namespace InfiniteRunner {
    public class PoolManager : MonoBehaviour {
        [Header("Coins Pool")]
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private int _coinCount = 100;
        private Queue<GameObject> _coinShelf = new Queue<GameObject>();

        [Header("Coins Container Pool")]
        [SerializeField] private GameObject _coinContainerPrefab;
        [SerializeField] private int _coinContainerCount = 10;
        private Queue<GameObject> _coinContainerShelf = new Queue<GameObject>();

        public static PoolManager Instance { get; private set; }

        private void Awake() {
            // jadi singleton
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);

            // prewarm shelf
            for (int i = 0; i < _coinCount; i++) {
                _coinShelf.Enqueue(CreateNew(_coinPrefab));
            }

            for (int i = 0; i < _coinContainerCount; i++) {
                _coinContainerShelf.Enqueue(CreateNew(_coinContainerPrefab));
            }
        }

        public GameObject GetAndSetPositionRotation(PoolType type, Vector3 position, Quaternion rotation, GameObject parent=null) {
            GameObject obj = null;

            switch (type) {
                case PoolType.Coin:
                    obj = _coinShelf.Dequeue();
                    break;
                case PoolType.CoinContainer:
                    obj = _coinContainerShelf.Dequeue();
                    break;
            }

            if (parent != null) {
                obj.transform.SetParent(parent.transform);
            }
            
            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);

            return obj;
        }

        public void Return(PoolType type, GameObject obj) {
            obj.SetActive(false);

            switch (type) {
                case PoolType.Coin:
                    _coinShelf.Enqueue(obj);
                    break;
                case PoolType.CoinContainer:
                    _coinContainerShelf.Enqueue(obj);
                    break;
            }
        }

        private GameObject CreateNew(GameObject prefab) {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            return obj;
        }
    }
}