using UnityEngine;

public enum ToolType { Axe, Pickaxe, Shovel, Hoe }

[CreateAssetMenu(fileName = "New Tool", menuName = "Items/Tool")]
public class ToolItem : Item
{
    [Header("Tool Stats")]
    public ToolType toolType;
    public int harvestPower;
    public float durability;
}