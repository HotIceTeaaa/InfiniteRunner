using UnityEngine;
using UnityEngine.InputSystem;

namespace InfiniteRunner {
    public class InputReader : MonoBehaviour {

        [Header("Actions")]
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _jumpAction;

        [Header("Script Lain")]
        [SerializeField] private PlayerController _playerControllerScript;
        
        private void Update() {
            if (_jumpAction.action.IsPressed()) {
                _playerControllerScript.HandleJumpInput();
            }

            if (_moveAction.action.IsPressed()) {
                Vector2 movement = _moveAction.action.ReadValue<Vector2>();
                _playerControllerScript.HandleLaneInput(movement);
            }
        }

        private void OnEnable() {
            _moveAction.action.Enable();
            _jumpAction.action.Enable();
        }

        private void OnDisable() {
            _moveAction.action.Disable();
            _jumpAction.action.Disable();
        }
    }
}
