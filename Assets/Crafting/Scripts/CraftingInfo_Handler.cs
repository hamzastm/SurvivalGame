using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingInfo_Handler : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI craftingDuration;


    public void SetInfo(Recipe recipe)
    {
        if (recipe == null)
        {
            ClearInfo();
            return;
        }
        itemIcon.sprite = recipe.itemToCraft.itemIcon;
        itemName.text = recipe.itemToCraft.itemName;
        itemDescription.text = recipe.itemToCraft.itemDescription;
        craftingDuration.text = $"Time: {recipe.craftingTime}s";
    }

    public void ClearInfo()
    {
        itemIcon.sprite = null;
        itemName.text = "";
        itemDescription.text = "";
        craftingDuration.text = "";
    }
}
