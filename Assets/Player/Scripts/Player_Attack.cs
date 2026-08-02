using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [SerializeField] private Input_Handler inputHandler;
    private Player_Vitals playerVitals;

    private float _nextAttackTime;

    private void Awake()
    {
        playerVitals = GetComponent<Player_Vitals>();
    }

    private void Update()
    {
        if (inputHandler == null) return;

        if (inputHandler.IsAttacking > 0.5f && Time.time >= _nextAttackTime)
        {
            if (PlayerHolding.Instance != null && PlayerHolding.Instance.HeldItem != null)
            {
                HandleUse(PlayerHolding.Instance.HeldItem);
                _nextAttackTime = Time.time + PlayerHolding.Instance.HeldItem.cooldown;
            }
        }
    }

    private void HandleUse(Item item)
    {
        if (playerVitals.currentStamina <= 0)
        {
            return;
        }
        else
        {
            switch (item.itemHeaviness)
            {
                case ItemHeaviness.Light:
                    playerVitals.UseStamina(5f);
                    break;
                case ItemHeaviness.Medium:
                    playerVitals.UseStamina(10f);
                    break;
                case ItemHeaviness.Heavy:
                    playerVitals.UseStamina(20f);
                    break;
                default: break;
            }

            switch (item)
            {
                case ToolItem tool:
                    HandleTool(tool);
                    break;

                case WeaponItem weapon:
                    HandleWeapon(weapon);
                    break;

                default:
                    Debug.Log($"Using generic item: {item.itemName}");
                    break;
            }
        }
        
    }

    private void HandleTool(ToolItem tool)
    {
        GameObject spawnedItem = PlayerHolding.Instance.CurrentSpawnedItem;
        if (spawnedItem != null && spawnedItem.TryGetComponent(out Animator itemAnimator))
        {
            itemAnimator.SetTrigger("Hit");
        }

        if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out RaycastHit hit, 1f))
        {
            if (hit.collider.TryGetComponent(out Object_Actions objectActions))
            {
                objectActions.PreformAction(tool);
            }
        }
    }

    private void HandleWeapon(WeaponItem weapon)
    {
        Debug.Log($"Attacked with weapon: {weapon.itemName} doing {weapon.damage} damage");
    }
}