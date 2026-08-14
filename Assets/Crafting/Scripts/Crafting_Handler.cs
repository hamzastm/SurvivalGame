using System.Collections.Generic;
using UnityEngine;

public class Crafting_Handler : MonoBehaviour
{
    [SerializeField] private GameObject recipesGrid;
    [SerializeField] private GameObject recipeInfo;

    [SerializeField] private List<Recipe> recipeList;

    [SerializeField] private GameObject recipeCell;

    private List<Crafting_Cell> allRecipeCells = new List<Crafting_Cell>();
    private Recipe selectedRecipe;

    [SerializeField] private Inventory_Handler Inventory_Handler;

    [SerializeField] private Input_Handler input_Handler;

    private void Awake()
    {
        UpdateRecipes();
    }


    private void Update()
    {
        setSelected();
    }

    private void UpdateRecipes()
    {
        foreach (Transform child in recipesGrid.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Recipe recipe in recipeList)
        {
            GameObject recipeInstance = Instantiate(recipeCell, recipesGrid.transform);
            if(recipeInstance.TryGetComponent<Crafting_Cell>(out Crafting_Cell craftingCell))
            {
                craftingCell.SetRecipe(recipe);
            }
        }
        selectedRecipe = null;
        allRecipeCells.Clear();
        allRecipeCells.AddRange(recipesGrid.GetComponentsInChildren<Crafting_Cell>());
    }

    private void UpdateRecipeInfo(Recipe recipe)
    {
        if (recipeInfo.TryGetComponent<CraftingInfo_Handler>(out CraftingInfo_Handler recipeInfoComponent))
            recipeInfoComponent.SetInfo(recipe);
    }

    private void setSelected()
    {
        if(input_Handler.ClickPressedThisFrame)
        {
            foreach(Crafting_Cell cell in allRecipeCells)
            {
                if (cell.hovering)
                {
                    selectedRecipe = cell.GetRecipe();
                    UpdateRecipeInfo(selectedRecipe);
                    break;
                }
            }
        }
    }

    public void CraftItem() 
    {
        if (selectedRecipe == null) return;

        if (Inventory_Handler.CheckIngredients(selectedRecipe))
        {
            foreach (Recipe.Ingredient ingredient in selectedRecipe.Ingredients)
            {
                Inventory_Handler.RemoveItem(ingredient.item, ingredient.quantity);
            }

            Inventory_Handler.AddItem(selectedRecipe.itemToCraft, selectedRecipe.quantityToCraft);
        }
    }
}
