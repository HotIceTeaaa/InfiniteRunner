using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionReference;

    [SerializeField] public bool isJumpKeyPressed;

    private InputAction jumpAction;

    private void Awake()
    {
        jumpAction = inputActionReference.FindAction("Jump");
    }

    private void Update()
    {
        isJumpKeyPressed = jumpAction.IsPressed();
    }
}