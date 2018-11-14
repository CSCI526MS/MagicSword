using UnityEngine;

public class EquipmentSlot : InventorySlot {
	public EquipmentType EquipmentType;

	protected override void OnValidate()
	{
		base.OnValidate();
		gameObject.name = EquipmentType.ToString() + " Slot";
	}

	public override bool CanReceiveItem(Item item)
	{
		if (item == null)
			return true;
		EquippableItem equippableItem = item as EquippableItem;
        Debug.Log(equippableItem.equipmentType);
        Debug.Log(EquipmentType);
        return equippableItem != null && equippableItem.equipmentType == EquipmentType;
	}
}
