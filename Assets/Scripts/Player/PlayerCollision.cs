using UnityEngine;

namespace InfiniteRunner {
    public class PlayerCollision : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Obsticles")){
                if (PlayerStates.Instance._isShielded) {
                    PlayerStates.Instance._isShielded = false;
                    Destroy(other.gameObject);
                } else {
                    GameManager.Instance.GameOver();
                }
            }
        }
    }
}
