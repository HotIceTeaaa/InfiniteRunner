using UnityEngine;

namespace InfiniteRunner {
    public class MultiplierPowerup : MonoBehaviour {
        [SerializeField] private float _duration;

        private void Update() {
            if (PlayerStates.Instance._isGameOver) {
                return;
            }

            float speed = GameManager.Instance.Speed;
            transform.Translate(Vector3.back * (speed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {

                PlayerStates.Instance._isScoreMultiplied = true;
                PlayerStates.Instance._scoreMultiplierDurationLeft = _duration;

                Destroy(gameObject);
            }
        }
    }
}
