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
            Destroy(this);
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

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            UpdateHeldItem();
        }
    }
#endif

    private void UpdateHeldItem()
    {
        if (_currentSpawnedItem != null)
        {
            Destroy(_currentSpawnedItem);
            _currentSpawnedItem = null;
        }

        if (_holdItem != null && _holdItem.itemPrefab != null)
        {
            Transform parentTransform = _holdPoint != null ? _holdPoint : transform;

            _currentSpawnedItem = Instantiate(_holdItem.itemPrefab, parentTransform, false);
            _currentSpawnedItem.transform.localPosition = Vector3.zero;
            _currentSpawnedItem.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.Log("No item to hold");
        }
    }
    public GameObject CurrentSpawnedItem => _currentSpawnedItem;
}