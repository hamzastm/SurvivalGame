using UnityEngine;

public class Player_Vitals : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    public float CurrentHealth { get; private set; } = 100f;

    [Header("Hunger Settings")]
    [SerializeField] private float maxHunger = 100f;
    [SerializeField] private float hungerDecreaseRate = 0.1f;
    public float CurrentHunger { get; private set; } = 100f;

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegenRate = 5f; // Amount regenerated per second
    [SerializeField] private float staminaRegenDelay = 1f; // Delay before regen starts after using stamina
    public float CurrentStamina { get; private set; } = 100f;

    public bool IsTired { get; private set; } = false;

    private float staminaRegenTimer = 0f;

    private void Update()
    {
        HandleHungerAndStarvation();
        HandleStaminaRegen();
    }

    private void HandleHungerAndStarvation()
    {
        // 1. Passive hunger depletion
        CurrentHunger -= hungerDecreaseRate * Time.deltaTime;

        // 2. Starvation mechanics when hunger hits 0
        if (CurrentHunger <= 0f)
        {
            CurrentHunger = 0f;
            TakeDamage(0.3f * Time.deltaTime); // Continuous damage

            // Optional: drain stamina when starving
            CurrentStamina = Mathf.Max(0f, CurrentStamina - (0.2f * Time.deltaTime));
        }
    }

    private void HandleStaminaRegen()
    {
        // If stamina was used recently, tick down the delay timer
        if (staminaRegenTimer > 0f)
        {
            staminaRegenTimer -= Time.deltaTime;
            return;
        }

        // Regenerate stamina if below max (and player isn't starving)
        if (CurrentStamina < maxStamina && CurrentHunger > 0f)
        {
            CurrentStamina += staminaRegenRate * Time.deltaTime;
            CurrentStamina = Mathf.Min(CurrentStamina, maxStamina);

            // Recover from tired status once stamina recovers enough (e.g. above 20%)
            if (IsTired && CurrentStamina >= maxStamina)
            {
                IsTired = false;
            }
        }
    }

    public void UseStamina(float staminaAmount)
    {
        CurrentStamina -= staminaAmount;
        staminaRegenTimer = staminaRegenDelay; // Reset the regen cooldown timer

        if (CurrentStamina <= 0f)
        {
            CurrentStamina = 0f;
            IsTired = true;

            // Extra hunger penalty when exhausting all stamina
            CurrentHunger -= hungerDecreaseRate * 2f;
            CurrentHunger = Mathf.Max(0f, CurrentHunger);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        CurrentHealth = Mathf.Max(0f, CurrentHealth - damageAmount);
        if (CurrentHealth <= 0f)
        {
            // Trigger player death logic here
        }
    }

    public void Heal(float healAmount)
    {
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + healAmount);
    }

    public void ConsumeFood(float foodAmount)
    {
        CurrentHunger = Mathf.Min(maxHunger, CurrentHunger + foodAmount);
    }
}