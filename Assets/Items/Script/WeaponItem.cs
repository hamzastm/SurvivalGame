using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Items/Weapon")]
public class WeaponItem : Item
{
    [Header("Weapon Stats")]
    public float damage;
    public float attackSpeed;
    public float criticalChance;
    public GameObject hitParticlePrefab;
}