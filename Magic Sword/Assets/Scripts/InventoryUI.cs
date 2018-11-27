using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
	// private SpriteRenderer spriteRenderer;
	[SerializeField] Text hpText;
    [SerializeField] Text mpText;
    [SerializeField] Text speedText;
	[SerializeField] Text attackText;
	[SerializeField] Text defenseText;

	[SerializeField] Text curHp;
    [SerializeField] Text curSpeed;
	[SerializeField] Text curAttack;
	[SerializeField] Text curDefense;

	public Inventory inventory;
	public EquipmentPanel equipmentPanel;

	[SerializeField] Image draggableItem;

	private InventorySlot draggedSlot;
	public InventorySlot selectedSlot;

    private GameObject playerObject;
    private Player player;

	void OnEnable() {
    	playerObject = GameObject.FindWithTag("Player");
		inventory = FindObjectOfType<Inventory>();
        Time.timeScale = 0f;
        hpText.text = player.playerStatus.CurrentHP + "/" + player.playerStatus.MaxHP.ToString();
        mpText.text = player.playerStatus.CurrentMP + "/" + player.playerStatus.MaxMP.ToString();
        speedText.text = player.playerStatus.speed.ToString();
        attackText.text = player.playerStatus.attack.ToString();
        defenseText.text = player.playerStatus.defense.ToString();
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
		equipmentPanel.OnPointerClickEvent += Click;
        player = FindObjectOfType<Player>();

        for (int i = 0; i < inventory.inventorySlots.Length; i += 1){
			// Begin Drag
			inventory.inventorySlots[i].OnBeginDragEvent += BeginDrag;
			// End Drag
			inventory.inventorySlots[i].OnEndDragEvent += EndDrag;
			// Drag
			inventory.inventorySlots[i].OnDragEvent += Drag;
			// Drop
			inventory.inventorySlots[i].OnDropEvent += Drop;

			inventory.inventorySlots[i].OnPointerClickEvent += Click;
        }
    }



	private void Click(InventorySlot inventorySlot) {
		    EquippableItem equippableItem = inventorySlot.Item as EquippableItem;
    
        if (equippableItem != null) {

            if (inventorySlot == selectedSlot)
            {

                // set selected slot to null
                // swap icon of inventorySlot
                selectedSlot = null;
                Sprite tmp = equippableItem.icon;

                equippableItem.icon = equippableItem.iconSelected;
                equippableItem.iconSelected = tmp;
                inventorySlot.Item = equippableItem;

                curHp.text = "0";
                curSpeed.text = "0";
                curAttack.text = "0";
                curDefense.text = "0";

            }
            else
            {

                if (selectedSlot != null)
                {
                    // update selected slot UI
                    EquippableItem selectedItem = selectedSlot.Item as EquippableItem;
                    Sprite tmpicon = selectedItem.icon;
                    selectedItem.icon = selectedItem.iconSelected;
                    selectedItem.iconSelected = tmpicon;
                    selectedSlot.Item = selectedItem;
                }

                selectedSlot = inventorySlot;


                // update current inventory slot UI
                Sprite tmp = equippableItem.icon;
                equippableItem.icon = equippableItem.iconSelected;
                equippableItem.iconSelected = tmp;
                inventorySlot.Item = equippableItem;

                curHp.text = equippableItem.properties[0].ToString();
                curSpeed.text = equippableItem.properties[1].ToString();
                curAttack.text = equippableItem.properties[2].ToString();
                curDefense.text = equippableItem.properties[3].ToString();
            }
		}
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
					UpdateStat(-1, dragItem.properties);
					dragItem.Unequip(this);
				}
				if (dropItem != null) {
				    UpdateStat(1, dropItem.properties);
					dropItem.Equip(this);
				}

			}

			if (dropItemSlot is EquipmentSlot) {
        
				if (dragItem != null) {
					UpdateStat(1, dragItem.properties);
					dragItem.Equip(this);
				}
				if (dropItem != null) {
					UpdateStat(-1, dropItem.properties);
					dropItem.Unequip(this);
				}
			}
			
      if (draggedSlot == selectedSlot) {
				selectedSlot = dropItemSlot;
        
			}
      else if (dropItemSlot == selectedSlot) {
			 	selectedSlot = draggedSlot;
			 }

			EquippableItem draggedItem = draggedSlot.Item;
			draggedSlot.Item = dropItemSlot.Item;
			dropItemSlot.Item = draggedItem;
		}
	}

	public void UpdateStat(int op, int[] value) {
        player.playerStatus.hpMaxValue += op* value[0];
        if (player.playerStatus.hpValue > player.playerStatus.hpMaxValue)
        {
            player.playerStatus.hpValue = player.playerStatus.hpMaxValue;
        }
        player.playerStatus.speed += op*value[1]/10;
        player.playerStatus.attack += op * value[2]/10;
        player.playerStatus.defense += op*value[3];
        RefreshStatText();
    }

    private void RefreshStatText(){
        hpText.text = player.playerStatus.hpValue.ToString() + "/" + player.playerStatus.MaxHP.ToString();
        speedText.text = player.playerStatus.speed.ToString();
        attackText.text = player.playerStatus.attack.ToString();
        defenseText.text = player.playerStatus.defense.ToString();
    }

	private void Equip(EquippableItem item)
	{
		if (inventory.RemoveItemFromSlots(item))
		{
			EquippableItem previousItem;
			if (equipmentPanel.AddItem(item, out previousItem))
			{
				if (previousItem != null)
				{
					inventory.AddItem(previousItem);
				}
			}
			else
			{
				inventory.AddItem(item);
			}
		}
	}

	private void Unequip(EquippableItem item)
	{
		if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
		{
            inventory.AddItem(item);
		}
	}

}
