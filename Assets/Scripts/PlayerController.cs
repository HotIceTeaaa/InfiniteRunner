using InfiniteRunner;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InfiniteRunner {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float _laneDistance = 3f;
        [SerializeField] private float _laneChangeSpeed = 10f;
        [SerializeField] private float _jumpForce = 7f;
        [SerializeField] private float _groundCheckDistance = 1.1f;

        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _moveThreshold = 0.5f;

        [SerializeField] private Animator _animator;

        private Rigidbody _rigidbody;
        private int _currentLane = 1;
        private float _targetLaneX;
        private bool _isGrounded;
        private bool _moveHeld;   // was the stick pushed sideways last frame?

        // untuk animasi
        private bool _isJumping = false;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            if (GameManager.Instance.IsGameOver) {  
                return;
            }

            CheckGrounded();
            MoveToLane();
            
        }

        public void HandleLaneInput(Vector2 movementInput) {
            float moveX = movementInput.x;

            // Edge detection: only act on the frame the stick crosses past the threshold.
            if (Mathf.Abs(moveX) >= _moveThreshold) {
                if (!_moveHeld) {
                    if (moveX < 0f) {
                        _currentLane = Mathf.Max(0, _currentLane - 1);
                        _animator.SetInteger("MoveDirection", -1);
                    } else {
                        _currentLane = Mathf.Min(2, _currentLane + 1);
                        _animator.SetInteger("MoveDirection", 1);
                    }

                    _moveHeld = true;
                }
                _moveHeld = false;   // stick returned to neutral; ready for the next push
            }

            _targetLaneX = (_currentLane - 1) * _laneDistance;
        }

        public void HandleJumpInput() {
            if (_isGrounded) 
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _isJumping = true;
                _animator.SetBool("JumpBool", true);
            }
        }
        private void CheckGrounded() {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);

            if(_isGrounded && _isJumping) {
                _isJumping = false;
                _animator.SetBool("JumpBool", false);
            }
        }

        private void MoveToLane() {
            Vector3 velocity = _rigidbody.linearVelocity;
            float distanceToLane = _targetLaneX - _rigidbody.position.x;
            velocity.x = distanceToLane * _laneChangeSpeed; 
            _rigidbody.linearVelocity = velocity;

            if (Mathf.Abs(distanceToLane) < 0.2f) 
            {
                _animator.SetInteger("MoveDirection", 0);
            }
        }
    }
}
