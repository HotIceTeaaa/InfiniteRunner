using UnityEngine;

namespace InfiniteRunner {
    public class Shield : MonoBehaviour {

        private void Update() {
            if (PlayerStates.Instance._isGameOver) {
                return;
            }

            float speed = GameManager.Instance.Speed;
            transform.Translate(Vector3.back * (speed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                PlayerStates.Instance._isShielded = true;

                Destroy(gameObject);
            }
        }
    }
}
