using System;
using UnityEngine;

namespace InfiniteRunner {
    public class MultiplierPowerup : MonoBehaviour {

        public static event Action OnScoreMultiplierCollect;

        private void Update() {
            if (PlayerStates.Instance._isGameOver) {
                return;
            }

            float speed = GameManager.Instance.Speed;
            transform.Translate(Vector3.back * (speed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                OnScoreMultiplierCollect.Invoke();

                PlayerStates.Instance._isScoreMultiplied = true;
                PlayerStates.Instance._scoreMultiplierDurationLeft = PlayerStates.Instance._scoreMultiplierDuration;

                Destroy(gameObject);
            }
        }
    }
}
