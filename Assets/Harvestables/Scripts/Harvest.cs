using UnityEngine;

public class Harvest : MonoBehaviour
{
    Object_Health health;


    private void Awake()
    {
        health = GetComponent<Object_Health>();
    }

    public void HandleHarvestDeath()
    {
        
    }

    public void HandleHarvest(ToolItem tool)
    {
        switch (gameObject.tag)
        {
            case "Tree":
                HandleTreeHarvest(tool);
                break;
            case "Rock":
                HandleRockHarvest(tool);
                break;
            default:
                Debug.Log("Unhandled harvestable tag");
                break;
        }
    }

    private void HandleTreeHarvest(ToolItem tool)
    {
        if(tool.toolType == ToolType.Axe)
        {
            health.Damage(tool.harvestPower);
        }
        else
        {
            Debug.Log("You need an axe to harvest this tree.");
        }
    }

    private void HandleRockHarvest(ToolItem tool)
    {
        if (tool.toolType == ToolType.Pickaxe)
        {
            health.Damage(tool.harvestPower);
        }
        else
        {
            Debug.Log("You need an axe to harvest this tree.");
        }
    }
}
