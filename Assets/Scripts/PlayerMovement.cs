using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float jumpForce;

    private bool isGrounded;
    public Vector3 spawnPoint;

    // Update is called once per frame
    void FixedUpdate() {
        jump();
    }


    private void jump() {
        if (isGrounded && inputReader.isJumpKeyPressed) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ground") {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Ground") {
            isGrounded = false;
        }

    }
}
