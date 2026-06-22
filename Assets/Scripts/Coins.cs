using System;
using UnityEngine;

namespace InfiniteRunner {
    public class Coins : MonoBehaviour {
        public static event Action OnCollect;

        [SerializeField] private string _playerTag;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag(_playerTag)) {
                OnCollect.Invoke();
                Destroy(gameObject);
            }
        }
        private void Update() {
            if (GameManager.Instance.IsGameOver) {
                return;
            }

            float speed = GameManager.Instance.Speed;
            transform.Translate(Vector3.back * (speed * Time.deltaTime), Space.World);
        }
    }
}

