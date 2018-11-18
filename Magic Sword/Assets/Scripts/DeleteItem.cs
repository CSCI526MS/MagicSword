using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeleteItem : MonoBehaviour {

  private static InventoryUI inventoryUI;
  
  void Start() {
    Debug.Log("should confirm delete button");
    if (inventoryUI == null)
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }
  }
  
  public void OnDelete() {

    
    if (inventoryUI != null && inventoryUI.selectedSlot != null) {
      Debug.Log("should confirm before deleting");  
      if (EditorUtility.DisplayDialog(
                "Delete Selected Item from Inventory",
                "Are you sure you want to delete the selected item?", "Yes", "No")) {
        
        if (!searchSelectedSlot(inventoryUI.selectedSlot)) {
          
          inventoryUI.inventory.RemoveItemFromSlots(inventoryUI.selectedSlot.item);
          inventoryUI.selectedSlot = null;
        } else {
          inventoryUI.UpdateStat(-1, inventoryUI.selectedSlot.item.properties);
          inventoryUI.equipmentPanel.RemoveItem(inventoryUI.selectedSlot.item);
          inventoryUI.selectedSlot = null;
        }
        
        
      } else {
        Debug.Log("do not delete");
      }  
    } else {
      EditorUtility.DisplayDialog("Please select an item first", "No item selected.", "OK");
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
