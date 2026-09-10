using UnityEngine;

public class PlayerHolding : MonoBehaviour
{
    [SerializeField] private Item _holdItem;
    [SerializeField] private Transform _holdPoint;

    [SerializeField] private GameObject handPrefab;

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

        if (_holdItem && _holdItem.itemHeldPrefab != null)
        {
            _currentSpawnedItem = Instantiate(_holdItem.itemHeldPrefab, _holdPoint, false);
        }
        else if (!_holdItem)
        {
            _currentSpawnedItem = Instantiate(handPrefab, _holdPoint, false);
        }
    }
    public GameObject CurrentSpawnedItem => _currentSpawnedItem;
}