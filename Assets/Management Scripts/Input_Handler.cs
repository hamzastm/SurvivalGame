using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Handler : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    public Vector2 MoveDir => inputActions.Player.Move.ReadValue<Vector2>();
    public Vector2 LookDir => inputActions.Player.Look.ReadValue<Vector2>();

    public float InventoryNavigation => inputActions.Player.InventoryNavigation.ReadValue<float>();

    public bool IsJumping => inputActions.Player.Jump.IsPressed();
    public bool IsAttacking => inputActions.Player.Attack.IsPressed();

    public bool JumpPressedThisFrame => inputActions.Player.Jump.WasPressedThisFrame();
    public bool AttackPressedThisFrame => inputActions.Player.Attack.WasPressedThisFrame();

    public bool InteractPressedThisFrame => inputActions.Player.Interact.WasPressedThisFrame() || inputActions.UI.Interact.WasPressedThisFrame();

    public bool ToggleInventoryPressedThisFrame =>
        inputActions.Player.ToggleInventory.WasPressedThisFrame() ||
        inputActions.UI.ToggleInventory.WasPressedThisFrame();

    public bool IsPlayerInputEnabled => inputActions.Player.enabled;


    //events

    public event Action<float> OnInventoryNavigated;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.InventoryNavigation.performed += HandleInventoryNav;
        EnablePlayerInput();
    }

    private void OnDisable()
    {
        inputActions.Player.InventoryNavigation.performed -= HandleInventoryNav;
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

    public void ToggleInputState()
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

    private void HandleInventoryNav(InputAction.CallbackContext ctx) => OnInventoryNavigated?.Invoke(ctx.ReadValue<float>());
}