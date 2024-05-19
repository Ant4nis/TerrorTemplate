using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon", fileName = "ItemWeapon")]
public class ItemWeapon : InventoryItem
{
    [Header("Weapon Configuration")] 
    [Tooltip("Reference of weapon scriptable object")] [SerializeField]
    public Weapon Weapon;
    
    public override void EquipItem()
    {
        WeaponManager.Instance.EquipWeapon(Weapon, ID);
    }
}




