using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Handler : MonoBehaviour
{
    public GameObject hotBarObj;
    public GameObject inventoryCellParent;

    private List<Cell_Handler> inventoryCells = new List<Cell_Handler>();
    private List<Cell_Handler> hotBarCells = new List<Cell_Handler>();
    private List<Cell_Handler> allCells = new List<Cell_Handler>();

    [SerializeField] private Input_Handler inputHandler;
    [SerializeField] private float rayCastDistance = 4f;
    private LayerMask interactableLayer = ~3;

    private GameObject lookingAtItem;

    private int currentHotBarIndex = 0;

    [SerializeField] private Image dragedIcon;
    private bool isDragging = false;
    private Cell_Handler dragedCell = null;

    private void Awake()
    {
        inventoryCells.AddRange(inventoryCellParent.GetComponentsInChildren<Cell_Handler>());
        hotBarCells.AddRange(hotBarObj.GetComponentsInChildren<Cell_Handler>());
        allCells.AddRange(inventoryCells);
        allCells.AddRange(hotBarCells);
    }

    private void Update()
    {
        DetectLookingAtItem();
        PickUpItem();
        PlayerHolding.Instance.HeldItem = hotBarCells[currentHotBarIndex].GetItem();

        StartDrag();
        UpdateDragItemPostion();
        EndDrag();

        SetItemOnScroll();
    }

    public void AddItem(Item item, int quantity)
    {
        int remainingQuantity = quantity;

        foreach (Cell_Handler cell in allCells)
        {
            if (cell.HasItem() && cell.GetItem() == item)
            {
                int currentQuantity = cell.GetQuantity();
                int maxQuantity = item.itemMaxValue;

                if (currentQuantity < maxQuantity)
                {
                    int spaceAvailable = maxQuantity - currentQuantity;
                    int quantityToAdd = Mathf.Min(spaceAvailable, remainingQuantity);

                    cell.setItem(item, currentQuantity + quantityToAdd);
                    remainingQuantity -= quantityToAdd;

                    if (remainingQuantity <= 0)
                        return;



                }
            }
        }

        foreach (Cell_Handler cell in allCells)
        {
            if (!cell.HasItem())
            {
                int qunitityToPlace = Mathf.Min(item.itemMaxValue, remainingQuantity);
                cell.setItem(item, qunitityToPlace);
                remainingQuantity -= qunitityToPlace;

                if (remainingQuantity <= 0)
                    return;
            }
        }

        if (remainingQuantity > 0)
        {
            Debug.LogWarning($"Not enough space in inventory to add {remainingQuantity} of {item.itemName}");
        }
    }

    public void PickUpItem()
    {
        if (inputHandler.InteractPressedThisFrame && lookingAtItem != null)
        {
            Item_Intract item = lookingAtItem.GetComponent<Item_Intract>();
            AddItem(item.GetItem(), item.GetQuantity());
            Destroy(item.gameObject);
        }
    }

    public void DetectLookingAtItem()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayCastDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out Item_Intract item))
            {
                lookingAtItem = item.gameObject;
            }
            else
            {
                lookingAtItem = null;
            }
        }
    }

    public void SetItemOnScroll()
    {
        if (inputHandler.InventoryNavigation < 0)
        {
            CycleHotBar(1);
        }
        else if (inputHandler.InventoryNavigation > 0)
        {
            CycleHotBar(-1);
        }

        foreach (Cell_Handler cell in hotBarCells)
        {
            if (cell == hotBarCells[currentHotBarIndex])
            {
                cell.HighLight();
            }
            else
            {
                cell.UnHighLight();
            }
        }
    }

    public void CycleHotBar(int direction)
    {
        currentHotBarIndex += direction;
        if (currentHotBarIndex >= hotBarCells.Count)
        {
            currentHotBarIndex = 0;
        }
        else if (currentHotBarIndex < 0)
        {
            currentHotBarIndex = hotBarCells.Count - 1;
        }
    }


    private void StartDrag()
    {
        if (inputHandler.ClickPressedThisFrame) 
        {
            Cell_Handler hoverd = GetHoverdCell();

            if(hoverd != null && hoverd.HasItem())
            {
                dragedCell = hoverd;
                isDragging = true;

                dragedIcon.sprite = dragedCell.GetItem().itemIcon;
                dragedIcon.color = new Color(1,1,1,0.5f);
                dragedIcon.enabled = true;
            }
        }
    }

    private void EndDrag()
    {
        if (inputHandler.ClickReleasedThisFrame && isDragging)
        {
            Cell_Handler hoverd = GetHoverdCell();

            if (hoverd != null)
            {
                HandleDrop(dragedCell, hoverd);
            }

            isDragging = false;
            dragedCell = null;
            dragedIcon.enabled = false;
            
        }
    }

    private void HandleDrop(Cell_Handler fromCell, Cell_Handler toCell)
    {
        if (fromCell == null || toCell == null || fromCell == toCell)
            return;

        if (toCell.HasItem() && fromCell.GetItem() == toCell.GetItem())
        {
            int max = toCell.GetItem().itemMaxValue;
            int space = max - toCell.GetQuantity();

            if (space > 0) 
            {
                int moveQuantity = Mathf.Min(space, fromCell.GetQuantity());
                toCell.AddQuantity(moveQuantity);
                fromCell.RemoveQuantity(moveQuantity);
            }
            return;
        }

        if (toCell.HasItem())
        {
            Item tempItem = toCell.GetItem();
            int tempQuantity = toCell.GetQuantity();

            toCell.setItem(fromCell.GetItem(), fromCell.GetQuantity());
            fromCell.setItem(tempItem, tempQuantity);
            return;
        }

        toCell.setItem(fromCell.GetItem(), fromCell.GetQuantity());
        fromCell.ClearItem();
    }

    private void UpdateDragItemPostion()
    {
        if (isDragging)
        {
            Vector3 mousePos = inputHandler.MousePosition;
            dragedIcon.transform.position = mousePos;
        }
    }

    private Cell_Handler GetHoverdCell()
    {
        foreach (Cell_Handler cell in allCells)
        {
            if (cell.hovering)
            {
                return cell;
            }
        }
        return null;
    }
}