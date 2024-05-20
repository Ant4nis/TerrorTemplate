using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// The EnemyLoot class manages the experience points and items that an enemy can drop upon defeat.
/// It handles the configuration of drop items and their probabilities, and initializes the list of items to be dropped.
/// </summary>
public class EnemyLoot : MonoBehaviour
{
    [Header("XP Configuration")] 
    [Tooltip("Experience enemy drop")] [SerializeField]
    private float expDrop;

    [Header("Loot Configuration")]
    [Tooltip("Drop item list")]
    [SerializeField]
    private DropItem[] dropItem;

    /// <summary>
    /// List of items that the enemy will drop.
    /// </summary>
    public List<DropItem> Items { get; private set; }

    /// <summary>
    /// Amount of experience points the enemy will drop.
    /// </summary>
    public float ExpDrop => expDrop;

    /// <summary>
    /// Initializes the enemy loot by loading the drop items.
    /// </summary>
    private void Start()
    {
        LoadDropItems();
    }

    /// <summary>
    /// Loads the drop items based on their drop chances.
    /// Items are added to the Items list if they meet the probability condition.
    /// </summary>
    private void LoadDropItems()
    {
        Items = new List<DropItem>();
        foreach (DropItem item in dropItem)
        {
            float probability = Random.Range(0f, 100f);
            if (probability <= item.DropChance)
            {
                Items.Add(item);
            }
        }
    }
}

/// <summary>
/// The DropItem class represents an item that can be dropped by an enemy.
/// It contains the item's name, reference to the inventory item (scriptable object), quantity, drop chance, and a flag indicating if it has been picked up.
/// </summary>
[Serializable]
public class DropItem
{
    [Header("Configuration")]
    [Tooltip("Item dropped name")]
    public string Name;

    [Tooltip("Item Reference (Scriptable object)")]
    public InventoryItem Item;

    [Tooltip("Item amount")]
    public int Quantity;

    [Header("Drop Chance")]
    [Tooltip("Probability to drop this item")]
    public float DropChance;

    /// <summary>
    /// Flag indicating whether the item has been picked up.
    /// </summary>
    public bool PickedItem { get; set; }
}
