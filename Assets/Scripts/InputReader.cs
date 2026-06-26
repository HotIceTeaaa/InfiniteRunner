using UnityEngine;
using UnityEngine.InputSystem;

namespace InfiniteRunner {
    public class InputReader : MonoBehaviour {

        [Header("Actions")]
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _jumpAction;
        [SerializeField] private InputActionReference _slideAction;

        [Header("Script Lain")]
        [SerializeField] private PlayerController _playerControllerScript;
        
        private void Update() {
            if (_jumpAction.action.WasPressedThisFrame()) {
                _playerControllerScript.HandleJumpInput();
            }

            if (_moveAction.action.WasPerformedThisFrame()) {
                Vector2 movement = _moveAction.action.ReadValue<Vector2>();
                _playerControllerScript.HandleLaneInput(movement);
            }

            if (_slideAction.action.WasPerformedThisFrame()) {
                _playerControllerScript.HandleSlideInput();
            }
        }

        private void OnEnable() {
            _moveAction.action.Enable();
            _jumpAction.action.Enable();
            _slideAction.action.Enable();
        }

        private void OnDisable() {
            _moveAction.action.Disable();
            _jumpAction.action.Disable();
            _slideAction.action.Disable();
        }
    }
}
