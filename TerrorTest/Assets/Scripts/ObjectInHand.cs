using UnityEngine;

/// <summary>
/// This class is used to store the ID and associated InventoryItem for an object in hand.
/// </summary>
public class ObjectInHand : MonoBehaviour
{
    /// <summary>
    /// The associated InventoryItem.
    /// </summary>
    public InventoryItem inventoryItem;

    /// <summary>
    /// The ID of the object.
    /// </summary>
    public string ID;

    /// <summary>
    /// Assigns the ID from the associated InventoryItem.
    /// </summary>
    private void Awake()
    {
        if (inventoryItem != null)
        {
            // Assign the ID from the InventoryItem
            ID = inventoryItem.ID;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} does not have an InventoryItem assigned.");
        }
    }
}