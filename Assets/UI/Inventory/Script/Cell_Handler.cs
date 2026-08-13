using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell_Handler : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private Image itemIcon;
    private TextMeshProUGUI quantityText;
    private Image highlight;


    private Item heldItem;
    private int itemQuantity;

    public bool hovering;

    private void Awake()
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        quantityText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        highlight = transform.GetChild(2).GetComponent<Image>();
    }

    public Item GetItem()
    {
        return heldItem;
    }

    public int GetQuantity()
    {
        return itemQuantity;
    }
    public void setItem(Item item , int quantity)
    {
        heldItem = item;
        itemQuantity = quantity;

        UpdateCell();
    }

    public void UpdateCell()
    {
        if (itemIcon == null && transform.childCount > 0)
            itemIcon = transform.GetChild(0).GetComponent<Image>();

        if (quantityText == null && transform.childCount > 1)
            quantityText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (heldItem != null)
        {
            itemIcon.enabled = true;
            itemIcon.sprite = heldItem.itemIcon;
            quantityText.text = itemQuantity.ToString();
        }
        else
        {
            itemIcon.enabled = false;
            quantityText.text = "";
        }
    }

    public int AddQuantity(int quantity)
    {
        itemQuantity += quantity;
        UpdateCell();
        return itemQuantity;
    }

    public int RemoveQuantity(int quantity)
    {
        itemQuantity -= quantity;
        if (itemQuantity <= 0)
        {
            ClearItem();
        }
        else
        {
            UpdateCell();
        }
        return itemQuantity;
    }

    public void ClearItem()
    {
        heldItem = null;
        itemQuantity = 0;
        UpdateCell();
    }

    public bool HasItem()
    {
        return heldItem != null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }

    public void HighLight()
    {
        highlight.enabled = true;
    }
    public void UnHighLight()
    {
        highlight.enabled = false;
    }
}
