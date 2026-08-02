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

public enum ItemHeaviness
{
    Light,
    Medium,
    Heavy
}
[CreateAssetMenu(fileName = "New Item", menuName = "Items/item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public ItemType itemType;
    public int itemValue;
    public int itemMaxValue;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    public float cooldown;
    public ItemHeaviness itemHeaviness;
}