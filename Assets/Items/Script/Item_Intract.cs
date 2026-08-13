using UnityEngine;

public class Item_Intract : MonoBehaviour 
{
    private int quantity = 1;
    [SerializeField] private Item item;

    public Item GetItem()
    {
        return item;
    }

    public int GetQuantity()
    {
        return quantity;
    }

}
