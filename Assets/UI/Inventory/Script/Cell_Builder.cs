using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellBuilder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quantity;
    [SerializeField] private Image icon;

    private void Awake()
    {
        quantity = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Build(Item item)
    {
        quantity.text = item.itemValue.ToString();
        icon.sprite = item.itemIcon;
    }
}
