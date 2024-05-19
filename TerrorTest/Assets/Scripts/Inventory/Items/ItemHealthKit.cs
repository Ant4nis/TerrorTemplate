using UnityEngine;

[CreateAssetMenu(fileName = "ItemHealthKit", menuName = "Items/Health Kit")]
public class ItemHealthKit : InventoryItem
{
    [Header("Configuration")] 
    [Tooltip("Amount of health to restore")] 
    public float HealthValue;

    public override bool UseItem()
    {
        if (GameManager.Instance.Player.PlayerHealth.CanRestoreHealth())
        {
            GameManager.Instance.Player.PlayerHealth.RestoreHealth(HealthValue);
            return true;
        }

        return false;
    }
}
