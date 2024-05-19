
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTrankimazin", menuName = "Items/Trankimazin")]
public class ItemTrankimazin : InventoryItem
{
    [Header("Configuration")] 
    [Tooltip("Amount of madness to loss")] 
    public float MadnessValue;

    public override bool UseItem()
    {
        if (GameManager.Instance.Player.PlayerMadness.CanRestoreMadness())
        {
            GameManager.Instance.Player.PlayerMadness.RestoreMadness(MadnessValue);
            return true;
        }

        return false;
    }
}
