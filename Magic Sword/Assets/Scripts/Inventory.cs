using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour {

    private static readonly int sizeLimit = GlobalStatic.sizeLimit;
    public Item[] itemList = new Item[sizeLimit];
    [SerializeField] public InventorySlot[] inventorySlots;
    [SerializeField] GameObject itemsParent;

    public bool addItem (EquippableItem item) {
		// Debug.Log("item put in the bag");
        Debug.Log("item.EquipmentType = " + item.EquipmentType);
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

    public bool removeItem (Item item) {

        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots[i].Item == item) {
                inventorySlots[i].Item = null;
                // itemList.Remove(item);
                return true;
            }
        }

        return false;
	}

    public Item[] getItemList() {
		return itemList;
	}

    private void OnValidate() {
        Debug.Log("itemsParent ");
        if (itemsParent != null) {

            Debug.Log("inventorySlots ");
            // itemsParent = GameObject.FindWithTag("InventoryPanel");
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
