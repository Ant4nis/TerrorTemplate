 using System;
 using TMPro;
 using UnityEngine;
 using UnityEngine.Serialization;
 using UnityEngine.UI;

 public class InventorySlot : MonoBehaviour
 {
     public static event Action<int> OnSlotSelectedEvent; 
     
     [Header("Slot Configuration")] 
     [Tooltip("The item icon")] [SerializeField]
     private Image itemIcon;
     [Tooltip("The quantity container to deactivate")] [SerializeField]
     private GameObject quantityContainer;
     [Tooltip("The text mesh pro quantity")] [SerializeField]
     private TextMeshProUGUI itemQuantityTMP;
     
     public int Index { get; set; }

     public void ClickSlot()
     {
         OnSlotSelectedEvent?.Invoke(Index);
     }
     
     public void UpdateSlot(InventoryItem item)
     {
         itemIcon.sprite = item.Icon;
         itemQuantityTMP.text = item.Quantity.ToString();
         itemIcon.preserveAspect = true;
     }

     public void ShowSlotInformation(bool value)
     {
         itemIcon.gameObject.SetActive(value);
         quantityContainer.gameObject.SetActive(value);
     }


 }
