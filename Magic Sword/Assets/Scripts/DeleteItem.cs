using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteItem : MonoBehaviour {

  private static InventoryUI inventoryUI;
  
  void Start() {
    if (inventoryUI == null)
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }
  }
  
  public void OnDelete() {

    
    if (inventoryUI != null && inventoryUI.selectedSlot != null) {
      Debug.Log("should confirm before deleting");  
      if (!searchSelectedSlot(inventoryUI.selectedSlot)) {
        inventoryUI.inventory.RemoveItemFromSlots(inventoryUI.selectedSlot.item);
        inventoryUI.selectedSlot = null;
      } else {
        inventoryUI.UpdateStat(-1, inventoryUI.selectedSlot.item.properties);
        inventoryUI.equipmentPanel.RemoveItem(inventoryUI.selectedSlot.item);
        inventoryUI.selectedSlot = null;
      }
    } 
  }
  
  
  public bool searchSelectedSlot(InventorySlot selectedSlot) {
    
    for (int i = 0; i < inventoryUI.equipmentPanel.equipmentSlots.Length; i++)
		{
			if (inventoryUI.equipmentPanel.equipmentSlots[i] == selectedSlot)
			{
				return true;
			}
		}
		return false;
  }
  
}
