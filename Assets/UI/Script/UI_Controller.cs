using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private Input_Handler inputHandler;

    [SerializeField] private GameObject inventoryPanel;

    private void Update()
    {
        if (inputHandler.ToggleInventoryPressedThisFrame)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            inputHandler.ToggleInventoryState();
        }
    }
}
