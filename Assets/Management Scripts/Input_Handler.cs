using UnityEngine;

public class Input_Handler : MonoBehaviour
{
    InputSystem_Actions inputActions;

    Vector2 movementInput;
    Vector2 lookInput;

    float isJumping;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    private void Update()
    {
        movementInput = inputActions.Player.Move.ReadValue<Vector2>();
        isJumping = inputActions.Player.Jump.ReadValue<float>();
        lookInput = inputActions.Player.Look.ReadValue<Vector2>();
    }

    public Vector2 MoveDir => movementInput;
    public float IsJumping => isJumping;

    public Vector2 LookDir => lookInput;

}
