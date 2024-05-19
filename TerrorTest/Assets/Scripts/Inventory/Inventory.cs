using System;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Inventory Config")] 
    [Tooltip("If you want to auto save and auto load inventory")] [SerializeField]
    private bool autoSaveInventory = true; 
    [Tooltip("The inventory content to load")] [SerializeField]
    private GameContent gameContent;
    [Tooltip("The number of inventory slots")] [SerializeField]
    private int inventorySize;
    [Tooltip("The array to store our items")] [SerializeField]
    private InventoryItem[] inventoryItems;

    [Header("Testing")] 
    public InventoryItem testItem;
    public int InventorySize => inventorySize;
    public InventoryItem[] InventoryItems => inventoryItems;

    private readonly string INVENTORY_KEY_DATA = "USER_INVENTORY";
    
    public void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
        VerifyItemsForDraw();
        
        //testin use in main menu "Continue"
        if (autoSaveInventory)
        {
            LoadInventory();
        }
        //DeleteInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddItem(testItem, 1);
        }
    }
    
    //Add Item
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
                    if(autoSaveInventory)
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
            AddItem(item,remainingAmount);
        }
        
        if(autoSaveInventory)
        {
            SaveInventory();
        }
    }

    public void UseItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].UseItem())
        {
            DecreaseItemStack(index);
        }
        
        if(autoSaveInventory)
        {
            SaveInventory();
        }
    }

    public void DestroyItem(int index)
    {
        if (inventoryItems[index] == null) return;

        inventoryItems[index].DestroyItem();
        inventoryItems[index] = null;
        InventoryUI.Instance.DrawItem(null, index);
        
        if(autoSaveInventory)
        {
            SaveInventory();
        }
    }

    public void EquipItem(int index)
    {
        if (inventoryItems[index] == null) return;

        if ((inventoryItems[index].ItemType != ItemType.Weapon) && (inventoryItems[index].ItemType != ItemType.Light)) return;
        inventoryItems[index].EquipItem();
    }
    
    private void AddItemFreeSlot(InventoryItem item, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null) continue; // continue to the next
            inventoryItems[i] = item.CopyItem();
            inventoryItems[i].Quantity = quantity;
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
            return;
        }
    }

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

    private void VerifyItemsForDraw()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                InventoryUI.Instance.DrawItem(null, i);
            }
        }
    }

    private InventoryItem ItemExistsInGameContent(string itemID)
    {
        if (gameContent == null || gameContent.GameItems == null || gameContent.GameItems.Length == 0)
        {
            Debug.LogError("GameContent no está configurado correctamente o está vacío.");
            return null;
        }

        // Iterar sobre todos los elementos de gameContent.GameItems
        for (int i = 0; i < gameContent.GameItems.Length; i++)
        {
            if (gameContent.GameItems[i].ID == itemID)
            {
                return gameContent.GameItems[i];
            }
        }

        return null;
    }


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

    
    private void SaveInventory()
    {
        InventoryData saveData = new InventoryData();
        saveData.ItemContent = new string[inventorySize];
        saveData.ItemQuantity = new int[inventorySize];

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

    public void DeleteInventory()
    {
        SaveGame.Delete(INVENTORY_KEY_DATA);
    }
}
