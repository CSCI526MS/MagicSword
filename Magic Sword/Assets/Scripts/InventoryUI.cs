using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

	// Use this for initialization
	// public List<GameObject> slots;
	[SerializeField] Inventory inventory;
	[SerializeField] EquipmentPanel equipmentPanel;

	[SerializeField] Image draggableItem;

	private InventorySlot draggedSlot;

	void OnEnable()
    {
		inventory = FindObjectOfType<Inventory>();

        // List<Item> itemList = FindObjectOfType<Inventory>().getItemList();
        // InventorySlot[] slotsList = FindObjectsOfType<InventorySlot>();

		equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
		equipmentPanel.OnBeginDragEvent += BeginDrag;
		equipmentPanel.OnEndDragEvent += EndDrag;
		equipmentPanel.OnDragEvent += Drag;
		equipmentPanel.OnDropEvent += Drop;

		// inventory.OnItemRightClickedEvent += EquipFromInventory;
		Debug.Log(inventory.inventorySlots.Length);
		for (int i = 0; i < inventory.inventorySlots.Length; i += 1){
            Debug.Log(i);
            // slotsList[i].image.sprite = itemList[i].icon;
			// inventory.inventorySlots[i].image.sprite = itemList[i].icon;

			inventory.inventorySlots[i].OnRightClickEvent += EquipFromInventory;

			// Begin Drag
			inventory.inventorySlots[i].OnBeginDragEvent += BeginDrag;

			// End Drag
			inventory.inventorySlots[i].OnEndDragEvent += EndDrag;

			// Drag
			inventory.inventorySlots[i].OnDragEvent += Drag;

			// Drop
			inventory.inventorySlots[i].OnDropEvent += Drop;









            /*
            GameObject loot = (GameObject)Resources.Load("Prefabs/loot");
            loot = Instantiate(loot) as GameObject;
            //GameObject item = GameObject.Find("ItemImg");
            //GameObject newItem = Instantiate(item) as GameObject;
            loot.transform.position = slots[i].transform.position;
            loot.transform.SetParent(slots[i].transform);
            Sprite imageSprite = Resources.Load<Sprite>("RPG_inventory_icons/" + itemList[i].itemId);
            loot.GetComponent<SpriteRenderer>().sprite = imageSprite;
            */
        }
    }


	private void Awake() {
		Debug.Log("Awake");
		// inventory.OnItemRightClickedEvent += EquipFromInventory;
		// equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
	}
	private void EquipFromInventory(InventorySlot inventorySlot)
	{

		// if (inventorySlot.Item is EquippableItem)
		// {
			Debug.Log("right click");
		// 	Equip((EquippableItem)inventorySlot.Item);
		// }
		// else if (itemSlot.Item is UsableItem)
		// {
		// 	UsableItem usableItem = (UsableItem)itemSlot.Item;
		// 	usableItem.Use(this);
		//
		// 	if (usableItem.IsConsumable)
		// 	{
		// 		inventory.RemoveItem(usableItem);
		// 		usableItem.Destroy();
		// 	}
		// }

		EquippableItem equippableItem = inventorySlot.Item as EquippableItem;
		if (equippableItem != null) {
			Equip(equippableItem);
		}
	}

	private void EquipmentPanelRightClick(InventorySlot inventorySlot)
	{
		// if (itemSlot.Item is EquippableItem)
		// {
		// 	Unequip((EquippableItem)itemSlot.Item);
		// }
		EquippableItem equippableItem = inventorySlot.Item as EquippableItem;
		if (equippableItem != null) {
			Unequip(equippableItem);
		}
	}

	private void BeginDrag(InventorySlot inventorySlot)
	{
		if (inventorySlot.Item != null)
		{
			draggedSlot = inventorySlot;
			draggableItem.sprite = inventorySlot.Item.icon;
			draggableItem.transform.position = Input.mousePosition;
			draggableItem.enabled = true;
			// draggableItem.gameObject.SetActive(true);
		}
	}

	private void Drag(InventorySlot inventorySlot)
	{
		if (draggableItem.enabled) {
			draggableItem.transform.position = Input.mousePosition;
		}

	}

	private void EndDrag(InventorySlot inventorySlot)
	{
		draggedSlot = null;
		draggableItem.enabled = false;
		// draggableItem.gameObject.SetActive(false);
	}

	private void Drop(InventorySlot dropItemSlot)
	{
		// if (dragItemSlot == null) return;
		//
		// if (dropItemSlot.CanAddStack(dragItemSlot.Item))
		// {
		// 	AddStacks(dropItemSlot);
		// }
		// else if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
		// {
		// 	SwapItems(dropItemSlot);
		// }


		if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item)) {
			EquippableItem dragItem = draggedSlot.Item as EquippableItem;
			EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

			if (draggedSlot is EquipmentSlot) {
				if (dragItem != null) dragItem.Unequip(this);
				if (dropItem != null) dropItem.Equip(this);
			}

			if (dropItemSlot is EquipmentSlot) {
				if (dragItem != null) dragItem.Equip(this);
				if (dropItem != null) dropItem.Unequip(this);
			}

			// statPanel.UpdateStatValues();

			Item draggedItem = draggedSlot.Item;
			draggedSlot.Item = dropItemSlot.Item;
			dropItemSlot.Item = draggedItem;
		}


	}
	private void Equip(EquippableItem item)
	{
		Debug.Log("equip");
		if (inventory.removeItem(item))
		{
			EquippableItem previousItem;
			Debug.Log("equip " + item.EquipmentType);
			if (equipmentPanel.AddItem(item, out previousItem))
			{
				if (previousItem != null)
				{
					inventory.addItem(previousItem);
					// previousItem.Unequip(this);
					// statPanel.UpdateStatValues();
				}
				// item.Equip(this);
				// statPanel.UpdateStatValues();
			}
			else
			{
				inventory.addItem(item);
			}
		}
	}

	private void Unequip(EquippableItem item)
	{
		if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
		{
			// item.Unequip(this);
			// statPanel.UpdateStatValues();
			inventory.addItem(item);
		}
	}

}
