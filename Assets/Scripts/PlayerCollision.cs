using UnityEngine;

namespace InfiniteRunner {
    public class PlayerCollision : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Obsticles")){
                GameManager.Instance.GameOver();
            }
        }
    }
}
