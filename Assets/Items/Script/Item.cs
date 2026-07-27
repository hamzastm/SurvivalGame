using UnityEngine;

public enum ItemType
{
    Tool,
    Weapon,
    Resource,
    Consumable,
    QuestItem,
    Miscellaneous
}

public abstract class Item : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public ItemType itemType;
    public int itemValue;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    public float cooldown;
}