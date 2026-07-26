using UnityEngine;

public class PlayerHolding : MonoBehaviour
{
    [SerializeField] private Item _holdItem;
    private GameObject _currentSpawnedItem;

    public Item HoldItem
    {
        get => _holdItem;
        set
        {
            if (_holdItem == value) return;

            _holdItem = value;
            UpdateHeldItem();
        }
    }

    private void Start()
    {
        // Spawns whatever item was set in the Inspector on startup
        UpdateHeldItem();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Updates the visual in Play Mode when you swap items directly in the Inspector
        if (Application.isPlaying)
        {
            UpdateHeldItem();
        }
    }
#endif

    private void UpdateHeldItem()
    {
        // 1. Clear previous item
        if (_currentSpawnedItem != null)
        {
            Destroy(_currentSpawnedItem);
        }

        // 2. Spawn new item under this hand transform
        if (_holdItem != null && _holdItem.itemPrefab != null)
        {
            _currentSpawnedItem = Instantiate(_holdItem.itemPrefab, transform);

            // Zero out position & rotation so it snaps perfectly to the hand
            _currentSpawnedItem.transform.localPosition = Vector3.zero;
            _currentSpawnedItem.transform.localRotation = Quaternion.identity;

            Debug.Log("Holding item: " + _holdItem.itemName);
        }
        else
        {
            Debug.Log("No item to hold");
        }
    }
}