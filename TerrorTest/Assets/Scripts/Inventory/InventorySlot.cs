using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// The InventorySlot class manages the visual representation and interaction of an inventory slot in the UI.
/// It updates the slot with item data, handles click events, and shows or hides slot information.
/// </summary>
public class InventorySlot : MonoBehaviour
{
    /// <summary>
    /// Event triggered when the inventory slot is selected.
    /// </summary>
    public static event Action<int> OnSlotSelectedEvent;

    [Header("Slot Configuration")] 
    [Tooltip("The item icon")] 
    [SerializeField]
    private Image itemIcon;
    
    [Tooltip("The quantity container to deactivate")] 
    [SerializeField]
    private GameObject quantityContainer;
    
    [Tooltip("The TextMeshPro component for displaying item quantity")] 
    [SerializeField]
    private TextMeshProUGUI itemQuantityTMP;
    
    /// <summary>
    /// The index of the slot in the inventory.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Handles the click event on the slot and invokes the OnSlotSelectedEvent.
    /// </summary>
    public void ClickSlot()
    {
        OnSlotSelectedEvent?.Invoke(Index);
    }
    
    /// <summary>
    /// Updates the slot with the given item data.
    /// Sets the item icon and quantity text.
    /// </summary>
    /// <param name="item">The inventory item to display in the slot.</param>
    public void UpdateSlot(InventoryItem item)
    {
        itemIcon.sprite = item.Icon;
        itemQuantityTMP.text = item.Quantity.ToString();
        itemIcon.preserveAspect = true;
    }

    /// <summary>
    /// Shows or hides the slot information based on the given value.
    /// </summary>
    /// <param name="value">True to show the slot information, false to hide.</param>
    public void ShowSlotInformation(bool value)
    {
        itemIcon.gameObject.SetActive(value);
        quantityContainer.gameObject.SetActive(value);
    }
}
