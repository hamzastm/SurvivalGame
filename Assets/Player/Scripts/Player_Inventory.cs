using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [SerializeField] private int maxInventorySize = 2;
    public List<Item> inventory = new List<Item>();

    public List<Item> hotBar = new List<Item>();
    private int currentHotBarIndex = 0;
    private int hotBarSize = 7;

    [SerializeField] private Input_Handler inputHandler;

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


    private void Start()
    {
        PlayerHolding.Instance.HeldItem = hotBar[currentHotBarIndex];
    }

    private void OnEnable()
    {
        if (inputHandler != null)
            inputHandler.OnInventoryNavigated += OnHotbarScroll;
    }

    private void OnDisable()
    {
        if (inputHandler != null)
            inputHandler.OnInventoryNavigated -= OnHotbarScroll;
    }

    private void EquipCurrentHotbarItem()
    {
        if (PlayerHolding.Instance == null) return;

        // Safe bounds check in case hotbar list is empty or smaller than size
        if (hotBar != null && currentHotBarIndex < hotBar.Count)
        {
            PlayerHolding.Instance.HeldItem = hotBar[currentHotBarIndex];
        }
        else
        {
            PlayerHolding.Instance.HeldItem = null;
        }
    }

    // Executed automatically ONLY when the player actually scrolls
    private void OnHotbarScroll(float scrollDirection)
    {
        if (scrollDirection > 0)
        {
            currentHotBarIndex = (currentHotBarIndex + 1) % hotBarSize;
        }
        else if (scrollDirection < 0)
        {
            currentHotBarIndex = (currentHotBarIndex - 1 + hotBarSize) % hotBarSize;
        }

        EquipCurrentHotbarItem();
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