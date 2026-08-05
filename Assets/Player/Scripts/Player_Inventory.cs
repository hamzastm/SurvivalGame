using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [SerializeField] private int maxInventorySize = 2;
    public List<Item> inventory = new List<Item>();

    [SerializeField] Inventory_UI inventory_UI;
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.transform.TryGetComponent(out Item_Actions itemActions))
        {
            // Pass a independent copy into the inventory
            bool fullyCollected = CollectItem(itemActions.GetItemCopy());

            if (fullyCollected)
            {
                itemActions.DestroyItem();
            }
        }
    }

    /// <summary>
    /// Attempts to add an item to the inventory. 
    /// Returns true if the item was completely picked up.
    /// </summary>
    bool CollectItem(Item item)
    {
        // 1. Try to stack onto existing, non-full stacks first
        Item existingItem = inventory.Find(i => i.itemName == item.itemName && i.itemValue < i.itemMaxValue);

        if (existingItem != null)
        {
            int spaceLeftInStack = existingItem.itemMaxValue - existingItem.itemValue;

            if (item.itemValue <= spaceLeftInStack)
            {
                // Fits completely into the existing stack
                existingItem.itemValue += item.itemValue;
                item.itemValue = 0;
                return true;
            }
            else
            {
                // Top off the existing stack, keep remaining value on incoming item
                existingItem.itemValue = existingItem.itemMaxValue;
                item.itemValue -= spaceLeftInStack;
            }
        }

        // 2. If there's still leftover quantity, try adding a new inventory slot
        if (inventory.Count < maxInventorySize)
        {
            inventory.Add(item);
            return true;
        }

        inventory_UI.RefreshUI();
        return false;
    }

    private void DebugInventory()
    {
        foreach (Item i in inventory)
        {
            Debug.Log($"Inventory contains: {i.itemName} with value {i.itemValue}");
        }
    }
}