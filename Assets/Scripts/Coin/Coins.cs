using System;
using UnityEngine;

namespace InfiniteRunner {
    public class Coins : MonoBehaviour {
        public static event Action OnCollect;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                OnCollect.Invoke();

                PoolManager.Instance.Return(PoolType.Coin, gameObject);
            }
        }
    }
}

