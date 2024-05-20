using UnityEngine;

/// <summary>
/// ScriptableObject that contains the array of game items.
/// Used to save/load and manage the items available in the game.
/// </summary>
[CreateAssetMenu]
public class GameContent : ScriptableObject
{
    /// <summary>
    /// Array of game items.
    /// </summary>
    public InventoryItem[] GameItems;

    public void DeleteInventory()
    {
        Inventory.Instance.DeleteInventory();
    }
}