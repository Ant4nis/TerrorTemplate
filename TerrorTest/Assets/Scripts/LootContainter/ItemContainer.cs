using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The ItemContainer class manages a collection of items that can be contained within an object, 
/// such as a chest or container. It handles the configuration of drop items and their probabilities, 
/// and initializes the list of items to be contained.
/// </summary>
public class ItemContainer : MonoBehaviour
{
    [Header("Loot Configuration")]
    [Tooltip("Drop item list")] [SerializeField]
    private DropItem[] dropItem;

    /// <summary>
    /// List of items that the container will hold.
    /// </summary>
    public List<DropItem> Items { get; private set; }

    /// <summary>
    /// Initializes the item container by loading the drop items.
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