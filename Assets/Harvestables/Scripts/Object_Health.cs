using UnityEngine;

public class Object_Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void Damage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        Debug.Log($"{gameObject.name} took {damageAmount} damage. Remaining health: {_currentHealth}");

        if (_currentHealth <= 0f)
        {
            gameObject.GetComponent<Harvest>().HandleHarvestDeath();
        }
    }
}