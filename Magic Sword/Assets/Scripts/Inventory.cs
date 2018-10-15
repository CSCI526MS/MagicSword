using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour {

	// Use this for initialization
	// itemList
    public List<Item> itemList = new List<Item>();
    [SerializeField] public InventorySlot[] inventorySlots;
    [SerializeField] GameObject itemsParent;


	// public event Action<Item> OnItemRightClickedEvent;
    //
    // private void Awake() {
    //     Debug.Log("hll");
	// 	for (int i = 0; i < inventorySlots.Length; i++) {
    //         Debug.Log("inside for loop");
	// 		inventorySlots[i].OnRightClickEvent += OnItemRightClickedEvent;
	// 	}
	// }

	private int sizeLimit = 18;
    public bool addItem (EquippableItem item) {
		// Debug.Log("item put in the bag");
        Debug.Log("item.EquipmentType = " + item.EquipmentType);
        Debug.Log("item.hp = " + item.properties[0]);
        Debug.Log("item.itemId = " + item.itemId);
        // if (item.itemId == "axe") {
        //
        // }
        // setStat(item, 20, 5, 0, 10);

        for (int i = 0; i < inventorySlots.Length; i++) {
            Debug.Log(inventorySlots[i].Item);
            if (inventorySlots[i].Item == null) {
                inventorySlots[i].Item = item;
                // itemList.Add(item);
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

    public List<Item> getItemList() {
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
		for (; i < itemList.Count && i < inventorySlots.Length; i++) {
			inventorySlots[i].Item = itemList[i];
		}

		for (; i < inventorySlots.Length; i++) {
			inventorySlots[i].Item = null;
		}
    }
}
