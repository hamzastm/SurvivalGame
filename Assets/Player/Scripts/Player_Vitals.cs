using UnityEngine;
using UnityEngine.UI;

public class Player_Vitals : MonoBehaviour
{
    private float maxHealth = 100f;
    private float maxHunger = 100f;
    private float maxStamina = 100f;
    public float currentHealth { get; private set; } = 100f;
    public float currentHunger { get; private set; } = 100f;
    public float currentStamina { get; private set; } = 100f;

    private void Update()
    {
        currentHunger -= Time.deltaTime * 0.1f;


        if(currentHunger <= 0)
            currentStamina -= Time.deltaTime * 0.2f;

        if(currentStamina <= 0)
        {
            currentHealth -= Time.deltaTime * 0.3f;
            currentHunger -= 5f;
        }
            
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void ConsumeFood(float foodAmount)
    {
        currentHunger += foodAmount;
        if (currentHunger > maxHunger)
        {
            currentHunger = maxHunger;
        }
    }

    public void UseStamina(float staminaAmount)
    {
        currentStamina -= staminaAmount;
        if (currentStamina < 0f)
        {
            currentStamina = 0f;
        }
    }
}
