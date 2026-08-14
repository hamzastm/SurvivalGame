using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Crafting_Cell : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private Image recipeIcon;
    private Image highlight;

    private Recipe heldRecipe;

    public bool hovering;

    private void Awake()
    {
        recipeIcon = transform.GetChild(0).GetComponent<Image>();
        highlight = transform.GetChild(1).GetComponent<Image>();
    }


    public void SetRecipe(Recipe recipe)
    {
        if (heldRecipe != null)
            return;

        heldRecipe = recipe;
        UpdateCell();
    }

    public Recipe GetRecipe()
    {
        return heldRecipe;
    }

    public void UpdateCell()
    {
        if (recipeIcon == null && transform.childCount > 0)
            recipeIcon = transform.GetChild(0).GetComponent<Image>();
        if (heldRecipe != null)
        {
            recipeIcon.enabled = true;
            recipeIcon.sprite = heldRecipe.itemToCraft.itemIcon;
        }
        else
        {
            recipeIcon.enabled = false;
        }
    }

    public void HighLight()
    {
        highlight.enabled = true;
    }
    public void UnHighLight()
    {
        highlight.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }

}
