using UnityEngine;

public enum EquipmentType
{
	Consume,
	Helmet,
	Weapon
}

[CreateAssetMenu]
public class EquippableItem : Item {

	public EquipmentType EquipmentType;

	public EquippableItem(string itemId, EquipmentType EquipmentType, float hp, float speed, float attack, float defense, Sprite icon) {
		this.itemId = itemId;
		this.EquipmentType = EquipmentType;
        this.hp = hp;
        this.speed = speed;
        this.attack = attack;
        this.defense = defense;
        this.icon = icon;
	}

	public void Equip(InventoryUI c)
	{
		Debug.Log("equip " + " ??? here ?");

		// if (StrengthBonus != 0)
		// 	c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
		// if (AgilityBonus != 0)
		// 	c.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));
		// if (IntelligenceBonus != 0)
		// 	c.Intelligence.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
		// if (VitalityBonus != 0)
		// 	c.Vitality.AddModifier(new StatModifier(VitalityBonus, StatModType.Flat, this));
		//
		// if (StrengthPercentBonus != 0)
		// 	c.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
		// if (AgilityPercentBonus != 0)
		// 	c.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModType.PercentMult, this));
		// if (IntelligencePercentBonus != 0)
		// 	c.Intelligence.AddModifier(new StatModifier(IntelligencePercentBonus, StatModType.PercentMult, this));
		// if (VitalityPercentBonus != 0)
		// 	c.Vitality.AddModifier(new StatModifier(VitalityPercentBonus, StatModType.PercentMult, this));
	}

	public void Unequip(InventoryUI c)
	{
		Debug.Log("unequip ");
		// c.Strength.RemoveAllModifiersFromSource(this);
		// c.Agility.RemoveAllModifiersFromSource(this);
		// c.Intelligence.RemoveAllModifiersFromSource(this);
		// c.Vitality.RemoveAllModifiersFromSource(this);
	}
}
