using UnityEngine;

public class Crafting_Intract : Interactable_Object
{
    [SerializeField] private GameObject craftingUI;
    [SerializeField] private Input_Handler inputHandler;
    [SerializeField] private UI_Controller UIController;
    
    public override void Interact()
    {
        UIController.ToggleCraftingUI();
    }

   
}
