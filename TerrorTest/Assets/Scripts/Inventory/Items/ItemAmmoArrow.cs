
using UnityEngine;

[CreateAssetMenu(fileName = "ItemAmmoArrow", menuName = "Items/Ammo Arrow")]
class ItemAmmoArrow : InventoryItem
{
    [Header("Configuration")] 
    [Tooltip("Amount of ammo to restore")] 
    public float AmmoValue;
    
}
