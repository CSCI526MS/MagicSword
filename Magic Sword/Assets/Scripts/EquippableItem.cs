using UnityEngine;
public enum EquipmentType
{
	Consume,
	Helmet,
	Weapon,
    Armor,
    Boots,
    Ring
}

[CreateAssetMenu]
public class EquippableItem : Item {

	public EquipmentType equipmentType;
    /*
	public EquippableItem(string itemId, EquipmentType EquipmentType, int hp, int speed, int attack, int defense, Sprite icon) {
		this.itemId = itemId;
		this.EquipmentType = EquipmentType;
        this.properties[0] = hp;
        this.properties[1] = speed;
        this.properties[2] = attack;
        this.properties[3] = defense;
        this.icon = icon;
	}
    */
	public void Equip(InventoryUI c)
	{
		//Debug.Log("equip " + " ??? here ?");
	}

	public void Unequip(InventoryUI c)
	{
		Debug.Log("unequip ");
    }
}
