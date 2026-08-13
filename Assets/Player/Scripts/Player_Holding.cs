using UnityEngine;

public class PlayerHolding : MonoBehaviour
{
    [SerializeField] private Item _holdItem;
    [SerializeField] private Transform _holdPoint;

    private GameObject _currentSpawnedItem;

    public static PlayerHolding Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public Item HeldItem
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
        UpdateHeldItem();
    }


    private void UpdateHeldItem()
    {
        if (_currentSpawnedItem != null)
        {
            Destroy(_currentSpawnedItem);
            _currentSpawnedItem = null;
        }

        if (_holdItem != null && _holdItem.itemHeldPrefab != null)
        {
            _currentSpawnedItem = Instantiate(_holdItem.itemHeldPrefab, _holdPoint, false);
            _currentSpawnedItem.transform.localPosition = Vector3.zero;
            _currentSpawnedItem.transform.localRotation = Quaternion.identity;
        }
    }
    public GameObject CurrentSpawnedItem => _currentSpawnedItem;
}