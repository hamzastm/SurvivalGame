using UnityEngine;

public class Crafting_Intract : Interactable_Object
{
    [SerializeField] private GameObject craftingUI;
    [SerializeField] private Input_Handler inputHandler;

    public override void Interact()
    {
        Debug.Log("Interacted with Crafting Table");
        ToggleCraftingUI();
    }

    private void ToggleCraftingUI()
    {
        craftingUI.SetActive(!craftingUI.activeSelf);
        inputHandler.ToggleInputState();
    }
}
