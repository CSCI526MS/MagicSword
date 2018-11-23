using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour {

    public EquippableItem[] itemList = new EquippableItem[18];
    [SerializeField] public InventorySlot[] inventorySlots;
    [SerializeField] GameObject itemsParent;

    public bool AddItem (EquippableItem item) {
		// Debug.Log("item put in the bag");
        Debug.Log("item.EquipmentType = " + item.equipmentType);
        Debug.Log("item.hp = " + item.properties[0]);
        Debug.Log("item.itemId = " + item.itemId);
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots[i].Item == null) {
                inventorySlots[i].Item = item;
                itemList[i]=item;
                return true;
            }
        }

        return false;
	}

    // private void setStat(EquippableItem item, float hp, float speed, float attack, float defense) {
    //
    //     item.hp = hp;
    //     item.speed = speed;
    //     item.attack = attack;
    //     item.defense = defense;
    // }


    public bool IsFull() {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots[i].Item == null) {
                return false;
            }
        }

        return true;
    }

    public bool RemoveItemFromSlots (Item item) {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots[i].Item == item) {
                inventorySlots[i].Item = null;
                return true;
            }
        }

        return false;
    }

    public Item[] GetItemList() {
		return itemList;
	}

    private void OnValidate() {
        if (itemsParent != null) {
            inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();
        }

        RefreshUI();
    }

    private void RefreshUI() {
        int i = 0;
        for (; i < itemList.Length && i < inventorySlots.Length; i++) {
			inventorySlots[i].Item = itemList[i];
		}

		for (; i < inventorySlots.Length; i++) {
			inventorySlots[i].Item = null;
		}
    }
}
