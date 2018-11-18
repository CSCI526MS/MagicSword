using UnityEngine;
using System;
public class EquipmentPanel : MonoBehaviour {

	[SerializeField] Transform equipmentSlotsParent;
	public EquipmentSlot[] equipmentSlots;

	public event Action<InventorySlot> OnPointerClickEvent;
	public event Action<InventorySlot> OnBeginDragEvent;
    public event Action<InventorySlot> OnEndDragEvent;
    public event Action<InventorySlot> OnDragEvent;
    public event Action<InventorySlot> OnDropEvent;

	public void Start() {
		for (int i = 0; i < equipmentSlots.Length; i++) {
			// equipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
			// equipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
			equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
			equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
			equipmentSlots[i].OnDragEvent += OnDragEvent;
			equipmentSlots[i].OnDropEvent += OnDropEvent;
			equipmentSlots[i].OnPointerClickEvent += OnPointerClickEvent;
		}
	}

	private void OnValidate()
	{
		equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
	}

	public bool AddItem(EquippableItem item, out EquippableItem previousItem)
	{
		Debug.Log(item.equipmentType);
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].EquipmentType == item.equipmentType)
			{
				previousItem = (EquippableItem)equipmentSlots[i].Item;
				equipmentSlots[i].Item = item;
				// equipmentSlots[i].Amount = 1;
				return true;
			}
		}
		previousItem = null;
		return false;
	}

	public bool RemoveItem(EquippableItem item)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].Item == item)
			{
				equipmentSlots[i].Item = null;
				return true;
			}
		}
		return false;
	}
}
