using InfiniteRunner;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InfiniteRunner {
    public class PlayerController : MonoBehaviour {

        [Header("Changing Lanes Related")]
        [SerializeField] private float _laneDistance = 3f;
        [SerializeField] private float _laneChangeSpeed = 10f;
        [SerializeField] private float _moveThreshold = 0.5f;

        [Header("Jump Related")]
        [SerializeField] private float _jumpForce = 7f;
        [SerializeField] private float _groundCheckDistance = 1.1f;
        [SerializeField] private LayerMask _groundLayer;

        [Header("Slide Related")]
        [SerializeField] private CapsuleCollider _playerCollider;
        [SerializeField] private float _currentTimer;
        [SerializeField] private float _slideDuration;

        [Header("Other")]
        [SerializeField] private Animator _animator;

        private Rigidbody _rigidbody;
        private int _currentLane = 1;
        private float _targetLaneX;
        private bool _isGrounded;
        private bool _moveHeld;   // was the stick pushed sideways last frame?

        // untuk ukuran collider pas sliding/berdiri
        private float _yStanding = 0.23f;
        private float _ySliding = 0.11f;
        private float _heightStanding = 0.48f;
        private float _heightSliding = 0.24f;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            ResetTimer();
        }

        private void FixedUpdate() {
            if (PlayerStates.Instance._isGameOver) {  
                return;
            }

            CheckGrounded();
            MoveToLane();
        }

        private void Update() {
            if (PlayerStates.Instance._isSliding) {
                DecrementTimer();

                if (_currentTimer < 0f) {
                    PlayerStates.Instance._isSliding = false;
                    _animator.SetBool("SlideBool", false);

                    ResetTimer();
                    SetColliderToRun();
                }
            }
        }

        public void HandleLaneInput(Vector2 movementInput) {
            float moveX = movementInput.x;

            // Edge detection: only act on the frame the stick crosses past the threshold.
            if (Mathf.Abs(moveX) >= _moveThreshold) {
                if (!_moveHeld) {
                    if (moveX < 0f) {
                        _currentLane = Mathf.Max(0, _currentLane - 1);
                        _animator.SetInteger("MoveDirection", -1);
                        
                        SFXManager.Instance.PlayChangeLanesSFX();
                    } else {
                        _currentLane = Mathf.Min(2, _currentLane + 1);
                        _animator.SetInteger("MoveDirection", 1);

                        SFXManager.Instance.PlayChangeLanesSFX();
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
                _animator.SetBool("JumpBool", true);

                PlayerStates.Instance._isJumping = true;
                SFXManager.Instance.PlayJumpSFX();
            }
        }
        public void HandleSlideInput() {
            if (_isGrounded) {
                SetColliderToSlide();                
                _animator.SetBool("SlideBool", true);

                PlayerStates.Instance._isSliding = true;
                SFXManager.Instance.PlaySlideSFX();
            }
        }
        private void CheckGrounded() {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);

            if(_isGrounded && PlayerStates.Instance._isJumping) {
                PlayerStates.Instance._isJumping = false;
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

        private void ResetTimer() {
            _currentTimer = _slideDuration;
        }

        private void DecrementTimer() {
            _currentTimer -= Time.deltaTime;
        }

        //urutannya HARUS height baru center
        private void SetColliderToSlide() {
            _playerCollider.height = _heightSliding;
            _playerCollider.center = new Vector3(0, _ySliding, 0);
            
        }

        //urutannya HARUS height baru center
        private void SetColliderToRun() {
            _playerCollider.height = _heightStanding;
            _playerCollider.center = new Vector3(0, _yStanding, 0);
            
        }
    }
}
