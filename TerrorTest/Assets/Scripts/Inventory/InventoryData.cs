using System;

/// <summary>
/// Stores the data for the inventory, including item IDs and their quantities.
/// </summary>
[Serializable]
public class InventoryData
{
    /// <summary>
    /// Array to store item IDs.
    /// </summary>
    public string[] ItemContent;

    /// <summary>
    /// Array to store item quantities.
    /// </summary>
    public int[] ItemQuantity;
}