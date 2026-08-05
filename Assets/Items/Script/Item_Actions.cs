using UnityEngine;

public class Item_Actions : MonoBehaviour
{
    // Assign in the Unity Inspector, or initialize clean
    public Item currentItem;

    /// <summary>
    /// Safely destroys the item object in the scene.
    /// </summary>
    public void DestroyItem()
    {
       Destroy(gameObject);
    }

    /// <summary>
    /// Returns a fresh copy of the item so inventory changes 
    /// don't corrupt ground/prefab data.
    /// </summary>
    public Item GetItemCopy()
    {
        // If Item is a ScriptableObject:
        if (currentItem is ScriptableObject)
        {
            return Instantiate(currentItem);
        }

        // If Item is a standard C# class, return a copy/new instance:
        return new Item
        {
            itemName = currentItem.itemName,
            itemValue = currentItem.itemValue,
            itemMaxValue = currentItem.itemMaxValue
            // Copy any other fields your Item class has here
        };
    }
}