using UnityEngine;

/// <summary>
/// The ItemWeapon class represents a weapon item that can be equipped by the player.
/// This class inherits from InventoryItem and overrides the EquipItem method to equip the weapon.
/// </summary>
[CreateAssetMenu(menuName = "Items/Weapon", fileName = "ItemWeapon")]
public class ItemWeapon : InventoryItem
{
    [Header("Weapon Configuration")] 
    [Tooltip("Reference of weapon scriptable object")] 
    [SerializeField]
    public Weapon Weapon;

    /// <summary>
    /// Equips the weapon by calling the WeaponManager to equip the specified weapon.
    /// </summary>
    public override void EquipItem()
    {
        WeaponManager.Instance.EquipWeapon(Weapon, ID);
    }
}