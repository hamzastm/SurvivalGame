using UnityEngine;

public class Object_Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }
            

        if (currentHealth <= 0f)
        {
            gameObject.TryGetComponent(out Object_Actions actions);
            actions.ActionPreformed();
        }
    }
}