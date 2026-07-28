using UnityEngine;

public class Object_Actions : MonoBehaviour
{
    [Header("Requirements")]
    [SerializeField] private ToolType requiredToolType;
    [SerializeField] private string wrongToolMessage = "You don't have the right tool for this!";

    private Object_Health objectHealth;


    private void Awake()
    {
        objectHealth = GetComponent<Object_Health>();
    }

    public void PreformAction(ToolItem tool)
    {
        if (tool.toolType == requiredToolType)
        {
            objectHealth.Damage(tool.harvestPower);
            Debug.Log($"You used {tool.itemName} on {objectHealth.currentHealth}!");
        }
        else
        {
            Debug.Log(wrongToolMessage);
        }
    }


    public void ActionPreformed()
    {
        Debug.Log($"{gameObject.name} has been harvested!");
        // Add any additional logic for when the object is harvested, like dropping items or playing an animation.
    }
}
