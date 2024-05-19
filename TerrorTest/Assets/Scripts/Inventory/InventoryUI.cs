using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//creation of slots 

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Slots Configuration")]
    [Tooltip("Slot Prefab")] [SerializeField]
    private InventorySlot slotPrefab;
    [Tooltip("Container")] [SerializeField]
    private Transform container;

    [SerializeField]
    private UIManager uiManager;
    
    [Header("Inventory Panel")] 
    [Tooltip("Game object with status panel")] 
    public GameObject inventoryPanel;
    
    [Header("Description Panel")]
    [Tooltip("Panel Description gameobject")] [SerializeField]
    private GameObject descriptionPanel;
    [Tooltip("Item description icon image")] [SerializeField]
    private Image itemIcon;
    [Tooltip("Item  description name text")] [SerializeField]
    private TextMeshProUGUI itemNameTMP;
    [Tooltip("Item description text")] [SerializeField]
    private TextMeshProUGUI itemDescriptionTMP;
    
    public InventorySlot CurrentSlot { get; set; }
    
    //list of inventoryslot
    private List<InventorySlot> slotList = new List<InventorySlot>();
    
    private void Start()
    {
        InitInventory();
    }

    private void InitInventory()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++)
        {
            InventorySlot slot= Instantiate(slotPrefab, container);
            slot.Index = i;
            slotList.Add(slot);
        }
    }

    public void OpenCloseInventoryPanel()
    {
        uiManager.basePanel.SetActive(!uiManager.basePanel.activeSelf);
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf == false)
        {
            descriptionPanel.SetActive(false);
            CurrentSlot = null;
        }
    }
    
    public void OpenInventoryPanel()
    {
        uiManager.basePanel.SetActive(true);
        inventoryPanel.SetActive(true);
    }
    
    
    public void UseItem()
    {
        if (CurrentSlot == null) return;
        Inventory.Instance.UseItem(CurrentSlot.Index);
    }

    public void EquipItem()
    {
        if (CurrentSlot == null) return;
        
        Inventory.Instance.EquipItem(CurrentSlot.Index);
    }

    public void DestroyItem()
    {
        if (CurrentSlot == null) return;
        Inventory.Instance.DestroyItem(CurrentSlot.Index);
    }

    public void ShowItemDescription(int index)
    {
        if (Inventory.Instance.InventoryItems[index] == null) return;
        descriptionPanel.SetActive(true);
        itemIcon.sprite = Inventory.Instance.InventoryItems[index].Icon;
        itemNameTMP.text = Inventory.Instance.InventoryItems[index].Name;
        itemDescriptionTMP.text = Inventory.Instance.InventoryItems[index].Description;
    }

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
