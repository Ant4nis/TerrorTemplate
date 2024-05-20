using System;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;

/// <summary>
/// Manages the player's inventory, including adding, using, equipping, and destroying items.
/// Also handles saving and loading the inventory state.
/// </summary>
public class Inventory : Singleton<Inventory>
{
    [Header("Inventory Config")]
    [Tooltip("If you want to auto save and auto load inventory")]
    [SerializeField]
    private bool autoSaveInventory = true; 
    
    [Tooltip("The inventory content to load")]
    [SerializeField]
    private GameContent gameContent;
    
    [Tooltip("The number of inventory slots")]
    [SerializeField]
    private int inventorySize;
    
    [Tooltip("The array to store our items")]
    [SerializeField]
    private InventoryItem[] inventoryItems;

    [Header("Testing")]
    public InventoryItem testItem;

    /// <summary>
    /// Gets the size of the inventory.
    /// </summary>
    public int InventorySize => inventorySize;
    
    /// <summary>
    /// Gets the array of inventory items.
    /// </summary>
    public InventoryItem[] InventoryItems => inventoryItems;

    private readonly string INVENTORY_KEY_DATA = "USER_INVENTORY";

    private void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
        VerifyItemsForDraw();

        if (autoSaveInventory)
        {
            LoadInventory();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddItem(testItem, 1);
        }
    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <param name="quantity">The quantity of the item to add.</param>
    public void AddItem(InventoryItem item, int quantity)
    {
        if (item == null || quantity <= 0) return;

        List<int> itemIndexes = CheckItemStock(item.ID);
        if (item.IsStackable && itemIndexes.Count > 0)
        {
            foreach (int index in itemIndexes)
            {
                int maxStack = item.MaxStack;
                if (inventoryItems[index].Quantity < maxStack)
                {
                    inventoryItems[index].Quantity += quantity;
                    if (inventoryItems[index].Quantity > maxStack)
                    {
                        int difference = inventoryItems[index].Quantity - maxStack;
                        inventoryItems[index].Quantity = maxStack;
                        AddItem(item, difference);
                    }

                    InventoryUI.Instance.DrawItem(inventoryItems[index], index);
                    if (autoSaveInventory)
                    {
                        SaveInventory();
                    }
                    return;
                }
            }
        }

        int quantityToAdd = quantity > item.MaxStack ? item.MaxStack : quantity;
        AddItemFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;

        if (remainingAmount > 0)
        {
            AddItem(item, remainingAmount);
        }

        if (autoSaveInventory)
        {
            SaveInventory();
        }
    }

    /// <summary>
    /// Uses an item from the inventory.
    /// </summary>
    /// <param name="index">The index of the item in the inventory.</param>
    public void UseItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].UseItem())
        {
            DecreaseItemStack(index);
        }

        if (autoSaveInventory)
        {
            SaveInventory();
        }
    }

    /// <summary>
    /// Destroys an item from the inventory.
    /// </summary>
    /// <param name="index">The index of the item in the inventory.</param>
    public void DestroyItem(int index)
    {
        if (inventoryItems[index] == null) return;

        inventoryItems[index].DestroyItem();
        inventoryItems[index] = null;
        InventoryUI.Instance.DrawItem(null, index);

        if (autoSaveInventory)
        {
            SaveInventory();
        }
    }

    /// <summary>
    /// Equips an item from the inventory.
    /// </summary>
    /// <param name="index">The index of the item in the inventory.</param>
    public void EquipItem(int index)
    {
        if (inventoryItems[index] == null) return;

        if (inventoryItems[index].ItemType != ItemType.Weapon && inventoryItems[index].ItemType != ItemType.Light) return;
        inventoryItems[index].EquipItem();
    }

    /// <summary>
    /// Adds an item to the first available free slot in the inventory.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <param name="quantity">The quantity of the item to add.</param>
    private void AddItemFreeSlot(InventoryItem item, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null) continue;
            inventoryItems[i] = item.CopyItem();
            inventoryItems[i].Quantity = quantity;
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
            return;
        }
    }

    /// <summary>
    /// Decreases the stack size of an item in the inventory.
    /// </summary>
    /// <param name="index">The index of the item in the inventory.</param>
    private void DecreaseItemStack(int index)
    {
        inventoryItems[index].Quantity--;
        if (inventoryItems[index].Quantity <= 0)
        {
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, index);
        }
        else
        {
            InventoryUI.Instance.DrawItem(inventoryItems[index], index);
        }
    }

    /// <summary>
    /// Checks if an item is already in stock in the inventory.
    /// </summary>
    /// <param name="itemID">The ID of the item to check.</param>
    /// <returns>A list of indices where the item is found in the inventory.</returns>
    private List<int> CheckItemStock(string itemID)
    {
        List<int> itemIndexes = new List<int>();

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].ID == itemID)
            {
                itemIndexes.Add(i);
            }
        }
        return itemIndexes;
    }

    /// <summary>
    /// Verifies items for drawing in the inventory UI.
    /// </summary>
    private void VerifyItemsForDraw()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
        }
    }

    /// <summary>
    /// Checks if an item exists in the game content.
    /// </summary>
    /// <param name="itemID">The ID of the item to check.</param>
    /// <returns>The inventory item if it exists, null otherwise.</returns>
    private InventoryItem ItemExistsInGameContent(string itemID)
    {
        if (gameContent == null || gameContent.GameItems == null || gameContent.GameItems.Length == 0)
        {
            Debug.LogError("GameContent is not configured correctly or is empty.");
            return null;
        }

        foreach (var item in gameContent.GameItems)
        {
            if (item.ID == itemID)
            {
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// Loads the inventory from saved data.
    /// </summary>
    private void LoadInventory()
    {
        if (SaveGame.Exists(INVENTORY_KEY_DATA))
        {
            InventoryData loadData = SaveGame.Load<InventoryData>(INVENTORY_KEY_DATA);
            for (int i = 0; i < inventorySize; i++)
            {
                if (loadData.ItemContent[i] != null)
                {
                    InventoryItem itemFromContent = ItemExistsInGameContent(loadData.ItemContent[i]);
                    if (itemFromContent != null)
                    {
                        inventoryItems[i] = itemFromContent.CopyItem();
                        inventoryItems[i].Quantity = loadData.ItemQuantity[i];
                        InventoryUI.Instance.DrawItem(inventoryItems[i], i);
                    }
                }
                else
                {
                    inventoryItems[i] = null;
                }
            }
        }
    }

    /// <summary>
    /// Saves the current state of the inventory.
    /// </summary>
    private void SaveInventory()
    {
        InventoryData saveData = new InventoryData
        {
            ItemContent = new string[inventorySize],
            ItemQuantity = new int[inventorySize]
        };

        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                saveData.ItemContent[i] = null;
                saveData.ItemQuantity[i] = 0;
            }
            else
            {
                saveData.ItemContent[i] = inventoryItems[i].ID;
                saveData.ItemQuantity[i] = inventoryItems[i].Quantity;
            }
        }

        SaveGame.Save(INVENTORY_KEY_DATA, saveData);
    }

    /// <summary>
    /// Deletes the saved inventory data.
    /// </summary>
    public void DeleteInventory()
    {
        SaveGame.Delete(INVENTORY_KEY_DATA);
    }
}
