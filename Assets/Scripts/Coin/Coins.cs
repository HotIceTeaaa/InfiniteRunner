using System;
using UnityEngine;

namespace InfiniteRunner {
    public class Coins : MonoBehaviour {
        public static event Action OnCollect;

        [SerializeField] private string _playerTag;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag(_playerTag)) {
                OnCollect.Invoke();

                PoolManager.Instance.Return(PoolType.Coin, gameObject);
            }
        }
    }
}

