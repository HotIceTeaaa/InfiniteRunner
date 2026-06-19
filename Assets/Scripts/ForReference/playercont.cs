using InfiniteRunner;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessRunner {
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float _laneDistance = 3f;
        [SerializeField] private float _laneChangeSpeed = 10f;
        [SerializeField] private float _jumpForce = 7f;
        [SerializeField] private float _groundCheckDistance = 1.1f;
        [SerializeField] private LayerMask _groundLayer;

        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _jumpAction;
        [SerializeField] private float _moveThreshold = 0.5f;   // how far the stick must tilt to count

        private Rigidbody _rigidbody;
        private int _currentLane = 1;
        private float _targetLaneX;
        private bool _isGrounded;
        private bool _moveHeld;   // was the stick pushed sideways last frame?

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable() {
            _moveAction.action.Enable();
            _jumpAction.action.Enable();
        }

        private void OnDisable() {
            _moveAction.action.Disable();
            _jumpAction.action.Disable();
        }

        private void Update() {
            if (GameManager.Instance.IsGameOver) {
                return;
            }

            HandleLaneInput();
            HandleJumpInput();
        }

        private void FixedUpdate() {
            if (GameManager.Instance.IsGameOver) {
                return;
            }

            CheckGrounded();
            MoveToLane();
        }

        private void HandleLaneInput() {
            float moveX = _moveAction.action.ReadValue<Vector2>().x;

            // Edge detection: only act on the frame the stick crosses past the threshold.
            if (Mathf.Abs(moveX) >= _moveThreshold) {
                if (!_moveHeld) {
                    if (moveX < 0f) {
                        _currentLane = Mathf.Max(0, _currentLane - 1);
                    } else {
                        _currentLane = Mathf.Min(2, _currentLane + 1);
                    }

                    _moveHeld = true;
                } else {
                }
                _moveHeld = false;   // stick returned to neutral; ready for the next push
            }

            _targetLaneX = (_currentLane - 1) * _laneDistance;
        }

        private void HandleJumpInput() {
            if (_jumpAction.action.WasPressedThisFrame() && _isGrounded) {
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        private void CheckGrounded() {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
        }

        private void MoveToLane() {
            Vector3 velocity = _rigidbody.linearVelocity;
            // Only steer on X. Y stays free for gravity/jumping; Z is frozen in the Rigidbody constraints.
            float distanceToLane = _targetLaneX - _rigidbody.position.x;
            velocity.x = distanceToLane * _laneChangeSpeed;
            _rigidbody.linearVelocity = velocity;
        }
    }
}
