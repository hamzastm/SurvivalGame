using UnityEngine;


public enum ItemType
{
    Weapon,
    Resource,
    Consumable,
    QuestItem,
    Miscellaneous
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public int itemValue;
    public int itemMaxValue;
    public Sprite itemIcon;
    public GameObject itemPrefab;
}
