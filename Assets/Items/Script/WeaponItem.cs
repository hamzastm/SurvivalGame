using UnityEngine;

public enum WeaponType { Sword, Bow, Staff, Dagger }

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]

public class WeaponItem : Item
{
    public float damage;
    public float attackSpeed;
    public float criticalChance;
    public GameObject hitParticlePrefab;
    public WeaponType weaponType;
}