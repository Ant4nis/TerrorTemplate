using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The LootButton class is responsible for configuring and handling the behavior of a loot button in the UI. 
/// It displays the item icon, name, and quantity for a lootable item and provides functionality to collect the item, 
/// adding it to the player's inventory and marking it as picked up.
/// </summary>
public class LootButton : MonoBehaviour
{
    [Header("Configuration")] 
    [Tooltip("Item image")] [SerializeField]
    private Image itemIcon;
    
    [Tooltip("Item Name")] [SerializeField]
    private TextMeshProUGUI itemName;

    [Tooltip("Amount")] [SerializeField]
    private TextMeshProUGUI itemQuantity;

    /// <summary>
    /// Reference to the DropItem associated with this loot button.
    /// Used for collecting the item.
    /// </summary>
    public DropItem ItemLoaded { get; private set; }

    /// <summary>
    /// Configures the loot button with the given DropItem's data.
    /// Sets the item icon, name, and quantity to display on the button.
    /// </summary>
    /// <param name="dropItem">The DropItem to be displayed on the button.</param>
    public void ConfigLootButton(DropItem dropItem)
    {
        ItemLoaded = dropItem;
        itemIcon.sprite = dropItem.Item.Icon;
        itemName.text = dropItem.Item.name;
        itemQuantity.text = dropItem.Item.Quantity.ToString();
    }

    /// <summary>
    /// Collects the item associated with this loot button.
    /// Adds the item to the player's inventory, marks it as picked, and destroys the button.
    /// </summary>
    public void CollectItem()
    {
        if (ItemLoaded == null) return;
        Inventory.Instance.AddItem(ItemLoaded.Item, ItemLoaded.Quantity);
        ItemLoaded.PickedItem = true; // If we come later, we don't want to show the item picked, only not picked
        Destroy(gameObject);
    }
}