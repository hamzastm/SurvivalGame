using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private Input_Handler inputHandler;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject craftingPanel;

    private GameObject currentActive = null;

    private void Update()
    {
        if (inputHandler.ToggleInventoryPressedThisFrame)
        {
            ToggleUI(inventoryPanel);
        }
    }

    public void ToggleCraftingUI()
    {
        ToggleUI(craftingPanel);
    }

    public void ToggleUI(GameObject targetPanel)
    {
        if (targetPanel == null) return;

        // If a DIFFERENT panel is open, block opening this one
        if (currentActive != null && currentActive != targetPanel)
        {
            return;
        }

        // Toggle state
        bool willBeActive = !targetPanel.activeSelf;
        targetPanel.SetActive(willBeActive);

        // Track active panel and set explicit state
        if (willBeActive)
        {
            currentActive = targetPanel;
            SetUIMode(true);
        }
        else
        {
            currentActive = null;
            SetUIMode(false);
        }
    }

    private void SetUIMode(bool isUIOpen)
    {
        inputHandler.ToggleInputState();

        // Set explicit cursor state rather than toggling blindly
        Cursor.visible = isUIOpen;
        Cursor.lockState = isUIOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}