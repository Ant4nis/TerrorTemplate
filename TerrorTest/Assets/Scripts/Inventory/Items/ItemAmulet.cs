using UnityEngine;

[CreateAssetMenu(menuName = "Items/Amulet", fileName = "ItemAmulet")]
public class ItemAmulet: InventoryItem
{
    [Header("Amulet Configuration")] 
    [Tooltip("Reference of amulet scriptable object")] [SerializeField]
    public Amulet Amulet;

    public override bool UseItem()
    {
        return base.UseItem();
        
    }
}
