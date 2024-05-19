
using UnityEngine;

[CreateAssetMenu(fileName = "ItemHotPotion", menuName = "Items/Hot Potion")]
public class ItemHotPotion : InventoryItem
{
    [Header("Configuration")] 
    [Tooltip("Amount of temperature to restore")] 
    public float TemperatureValue;

    public override bool UseItem()
    {
        if (GameManager.Instance.Player.PlayerTemperature.CanRestoreTemperature())
        {
            GameManager.Instance.Player.PlayerTemperature.RestoreTemperature(TemperatureValue);
            return true;
        }

        return false;
    }
}
