using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

	[SerializeField] Text hpText;
	[SerializeField] Text speedText;
	[SerializeField] Text attackText;
	[SerializeField] Text defenseText;

	[SerializeField] Inventory inventory;
	[SerializeField] EquipmentPanel equipmentPanel;

	[SerializeField] Image draggableItem;

	private InventorySlot draggedSlot;
	private GameObject player;

	void OnEnable() {
    	player = GameObject.FindWithTag("Player");
		inventory = FindObjectOfType<Inventory>();
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }


    private void Awake() {
		equipmentPanel.OnBeginDragEvent += BeginDrag;
		equipmentPanel.OnEndDragEvent += EndDrag;
		equipmentPanel.OnDragEvent += Drag;
		equipmentPanel.OnDropEvent += Drop;

		for (int i = 0; i < inventory.inventorySlots.Length; i += 1){
			// Begin Drag
			inventory.inventorySlots[i].OnBeginDragEvent += BeginDrag;
			// End Drag
			inventory.inventorySlots[i].OnEndDragEvent += EndDrag;
			// Drag
			inventory.inventorySlots[i].OnDragEvent += Drag;
			// Drop
			inventory.inventorySlots[i].OnDropEvent += Drop;
        }
		hpText.text = "100";
		speedText.text  = "5";
		attackText.text  = "10";
		defenseText.text  = "0";
	}

	private void EquipmentPanelRightClick(InventorySlot inventorySlot)
	{
		EquippableItem equippableItem = inventorySlot.Item as EquippableItem;
		if (equippableItem != null) {
			Unequip(equippableItem);
		}
	}

	private void BeginDrag(InventorySlot inventorySlot)
	{
		FindObjectOfType<AudioManager>().Play("equip");
		if (inventorySlot.Item != null)
		{
			draggedSlot = inventorySlot;
			draggableItem.sprite = inventorySlot.Item.icon;
			draggableItem.transform.position = Input.mousePosition;
			draggableItem.enabled = true;
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
		FindObjectOfType<AudioManager>().Play("equip");
		draggedSlot = null;
		draggableItem.enabled = false;
	}

	private void Drop(InventorySlot dropItemSlot)
	{
		if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item)) {
			EquippableItem dragItem = draggedSlot.Item as EquippableItem;
			EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

			if (draggedSlot is EquipmentSlot) {
				if (dragItem != null) {
					player.SendMessage("Decline", dragItem.properties);
					hpText.text = updateStat(hpText.text, -dragItem.properties[0]);
					speedText.text = updateStat(speedText.text, -dragItem.properties[1]);
					attackText.text = updateStat(attackText.text, -dragItem.properties[2]);
					defenseText.text = updateStat(defenseText.text, -dragItem.properties[3]);
					dragItem.Unequip(this);
				}
				if (dropItem != null) {
					player.SendMessage("Improve", dragItem.properties);
					hpText.text = updateStat(hpText.text, dropItem.properties[0]);
					speedText.text = updateStat(speedText.text, dropItem.properties[1]);
					attackText.text = updateStat(attackText.text, dropItem.properties[2]);
					defenseText.text = updateStat(defenseText.text, dropItem.properties[3]);
					dropItem.Equip(this);
				}

			}

			if (dropItemSlot is EquipmentSlot) {
				if (dragItem != null) {
					player.SendMessage("Improve", dragItem.properties);
					hpText.text = updateStat(hpText.text, dragItem.properties[0]);
					speedText.text = updateStat(speedText.text, dragItem.properties[1]);
					attackText.text = updateStat(attackText.text, dragItem.properties[2]);
					defenseText.text = updateStat(defenseText.text, dragItem.properties[3]);

					dragItem.Equip(this);
				}
				if (dropItem != null) {
					player.SendMessage("Decline", dragItem.properties);
					hpText.text = updateStat(hpText.text, -dropItem.properties[0]);
					speedText.text = updateStat(speedText.text, -dropItem.properties[1]);
					attackText.text = updateStat(attackText.text, -dropItem.properties[2]);
					defenseText.text = updateStat(defenseText.text, -dropItem.properties[3]);
					dropItem.Unequip(this);
				}
			}

			Item draggedItem = draggedSlot.Item;
			draggedSlot.Item = dropItemSlot.Item;
			dropItemSlot.Item = draggedItem;
		}
	}

	private string updateStat(string text, float value) {
		text = (float.Parse(text) + value).ToString();
		return text;
	}

	private void Equip(EquippableItem item)
	{
		if (inventory.removeItem(item))
		{
			EquippableItem previousItem;
			if (equipmentPanel.AddItem(item, out previousItem))
			{
				if (previousItem != null)
				{
					inventory.addItem(previousItem);
				}
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
			inventory.addItem(item);
		}
	}

}
