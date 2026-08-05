using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory_UI : MonoBehaviour
{
    [SerializeField] private Player_Inventory playerInventoryScript;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform gridContainer; 

    private void Awake()
    {
        if (playerInventoryScript == null && playerInventoryScript != null)
        {
            playerInventoryScript = playerInventoryScript.GetComponent<Player_Inventory>();
        }

        if (gridContainer == null)
        {
            gridContainer = transform.GetChild(0);
        }
    }


    private void OnEnable()
    {
        RefreshUI();
    }

    /// <summary>
    /// Call this method whenever the player collects or drops an item!
    /// </summary>
    public void RefreshUI()
    {

        if (playerInventoryScript == null) return;

        foreach (Transform child in gridContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in playerInventoryScript.inventory)
        {
            GameObject newCell = Instantiate(cellPrefab, gridContainer);

            Debug.Log($"Instantiated new cell for item: {item.itemName}, Quantity: {item.itemValue} , {newCell.GetComponent<CellBuilder>()}");

             if (newCell.TryGetComponent(out CellBuilder cellBuilder))
            {
                cellBuilder.Build(item);
            }
        }
    }
}