using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the inventory UI, including the creation and updating of inventory slots,
/// and handling user interactions with the inventory.
/// </summary>
public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Slots Configuration")]
    [Tooltip("Slot Prefab")] 
    [SerializeField]
    private InventorySlot slotPrefab;
    
    [Tooltip("Container")] 
    [SerializeField]
    private Transform container;

    [SerializeField]
    private UIManager uiManager;
    
    [Header("Inventory Panel")] 
    [Tooltip("Game object with status panel")] 
    public GameObject inventoryPanel;
    
    [Header("Description Panel")]
    [Tooltip("Panel Description gameobject")] 
    [SerializeField]
    private GameObject descriptionPanel;
    
    [Tooltip("Item description icon image")] 
    [SerializeField]
    private Image itemIcon;
    
    [Tooltip("Item description name text")] 
    [SerializeField]
    private TextMeshProUGUI itemNameTMP;
    
    [Tooltip("Item description text")] 
    [SerializeField]
    private TextMeshProUGUI itemDescriptionTMP;
    
    /// <summary>
    /// The currently selected inventory slot.
    /// </summary>
    public InventorySlot CurrentSlot { get; set; }
    
    /// <summary>
    /// List of all inventory slots in the UI.
    /// </summary>
    private List<InventorySlot> slotList = new List<InventorySlot>();

    private void Start()
    {
        InitInventory();
    }

    /// <summary>
    /// Initializes the inventory UI by creating slots based on the inventory size.
    /// </summary>
    private void InitInventory()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.Index = i;
            slotList.Add(slot);
        }
    }

    /// <summary>
    /// Toggles the visibility of the inventory panel.
    /// Used for button from UI and keyboard.
    /// </summary>
    public void OpenCloseInventoryPanel()
    {
        uiManager.basePanel.SetActive(!uiManager.basePanel.activeSelf);
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (!inventoryPanel.activeSelf)
        {
            descriptionPanel.SetActive(false);
            CurrentSlot = null;
        }
    }
    
    /// <summary>
    /// Opens the inventory panel.
    /// Used for button from base panel.
    /// </summary>
    public void OpenInventoryPanel()
    {
        uiManager.basePanel.SetActive(true);
        inventoryPanel.SetActive(true);
    }
    
    /// <summary>
    /// Uses the item in the currently selected slot.
    /// </summary>
    public void UseItem()
    {
        if (CurrentSlot == null) return;
        Inventory.Instance.UseItem(CurrentSlot.Index);
    }

    /// <summary>
    /// Equips the item in the currently selected slot.
    /// </summary>
    public void EquipItem()
    {
        if (CurrentSlot == null) return;
        Inventory.Instance.EquipItem(CurrentSlot.Index);
    }

    /// <summary>
    /// Destroys the item in the currently selected slot.
    /// </summary>
    public void DestroyItem()
    {
        if (CurrentSlot == null) return;
        Inventory.Instance.DestroyItem(CurrentSlot.Index);
    }

    /// <summary>
    /// Displays the description of the item at the given index.
    /// </summary>
    /// <param name="index">The index of the item in the inventory.</param>
    public void ShowItemDescription(int index)
    {
        if (Inventory.Instance.InventoryItems[index] == null) return;
        descriptionPanel.SetActive(true);
        itemIcon.sprite = Inventory.Instance.InventoryItems[index].Icon;
        itemNameTMP.text = Inventory.Instance.InventoryItems[index].Name;
        itemDescriptionTMP.text = Inventory.Instance.InventoryItems[index].Description;
    }

    /// <summary>
    /// Updates the slot at the given index with the provided item.
    /// </summary>
    /// <param name="item">The item to display in the slot.</param>
    /// <param name="index">The index of the slot to update.</param>
    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slotList[index];
        if (item == null)
        {
            slot.ShowSlotInformation(false);
            return;
        }
        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }
    
    /// <summary>
    /// Callback method for when a slot is selected.
    /// Updates the current slot and displays the item description.
    /// </summary>
    /// <param name="slotIndex">The index of the selected slot.</param>
    private void SlotSelectedCallback(int slotIndex)
    {
        CurrentSlot = slotList[slotIndex];
        ShowItemDescription(slotIndex);
    }

    private void OnEnable()
    {
        InventorySlot.OnSlotSelectedEvent += SlotSelectedCallback;
    }

    private void OnDisable()
    {
        InventorySlot.OnSlotSelectedEvent -= SlotSelectedCallback;
    }
}
