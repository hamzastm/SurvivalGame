using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private Input_Handler inputHandler;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject craftingPanel;

    private void Update()
    {
        if (inputHandler.ToggleInventoryPressedThisFrame)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            inputHandler.ToggleInputState();
            Cursor.visible = Cursor.visible ? false : true;
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void OpenCraftingUI()
    {
        craftingPanel.SetActive(true);
        inputHandler.EnableUIInput();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
