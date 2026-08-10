using UnityEngine;

public class Input_Handler : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    public Vector2 MoveDir => inputActions.Player.Move.ReadValue<Vector2>();
    public Vector2 LookDir => inputActions.Player.Look.ReadValue<Vector2>();

    public bool IsJumping => inputActions.Player.Jump.IsPressed();
    public bool IsAttacking => inputActions.Player.Attack.IsPressed();

    public bool JumpPressedThisFrame => inputActions.Player.Jump.WasPressedThisFrame();
    public bool AttackPressedThisFrame => inputActions.Player.Attack.WasPressedThisFrame();

    public bool InteractPressedThisFrame => inputActions.Player.Interact.WasPressedThisFrame();

    public bool ToggleInventoryPressedThisFrame =>
        inputActions.Player.ToggleInventory.WasPressedThisFrame() ||
        inputActions.UI.ToggleInventory.WasPressedThisFrame();

    public bool IsPlayerInputEnabled => inputActions.Player.enabled;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        EnablePlayerInput();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.UI.Disable();
    }

    public void EnableUIInput()
    {
        inputActions.Player.Disable();
        inputActions.UI.Enable();
    }

    public void EnablePlayerInput()
    {
        inputActions.UI.Disable();
        inputActions.Player.Enable();
    }

    public void ToggleInventoryState()
    {
        if (inputActions.Player.enabled)
        {
            EnableUIInput();
        }
        else
        {
            EnablePlayerInput();
        }
    }
}