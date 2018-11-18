using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drops : MonoBehaviour {
    public LayerMask enemyLayer;
    private static Dictionary<string, EquippableItem> dic;
    public EquippableItem item;
    public GameObject[] cubes;

    void Awake() {
        dic = new Dictionary<string, EquippableItem>();
        // [hp, speed ,attack, defense]
        // hp
        EquippableItem hp_potion_0 = GenerateItem("hp_potion_0", new int[]{30, 0, 0, 0}, EquipmentType.Consume);
        EquippableItem hp_potion_1 = GenerateItem("hp_potion_1", new int[]{50, 0, 0, 0}, EquipmentType.Consume);
        EquippableItem hp_potion_2 = GenerateItem("hp_potion_2", new int[]{70, 0, 0, 0}, EquipmentType.Consume);
        
        // weapon
        EquippableItem weapon_item_0 = GenerateItem("weapon_0", new int[]{0, 0, 10, 0}, EquipmentType.Weapon);
        EquippableItem weapon_item_1 = GenerateItem("weapon_1", new int[]{0, 0, 30, 0}, EquipmentType.Weapon);
        EquippableItem weapon_item_2 = GenerateItem("weapon_2", new int[]{0, 0, 70, 0}, EquipmentType.Weapon);
        
        // helmets
        EquippableItem helmet_item_0 = GenerateItem("helmet_0", new int[]{0, 0, 0, 10}, EquipmentType.Helmet);
        EquippableItem helmet_item_1 = GenerateItem("helmet_1", new int[]{0, 0, 0, 30}, EquipmentType.Helmet);

        // boots
        EquippableItem boots_item_0 = GenerateItem("boots_0", new int[]{0, 10, 0, 0}, EquipmentType.Boots);

        // armor
        EquippableItem armor_item_0 = GenerateItem("armor_0", new int[]{0, 0, 0, 10}, EquipmentType.Armor);
        EquippableItem armor_item_1 = GenerateItem("armor_1", new int[]{0, 0, 0, 30}, EquipmentType.Armor);

        dic.Add("hp_potion_0", hp_potion_0);
        dic.Add("hp_potion_1", hp_potion_1);
        dic.Add("hp_potion_2", hp_potion_2);
        dic.Add("weapon_0", weapon_item_0);
        dic.Add("weapon_1", weapon_item_1);
        dic.Add("weapon_2", weapon_item_2);
        dic.Add("helmet_0", helmet_item_0);
        dic.Add("helmet_1", helmet_item_1);
        dic.Add("boots_0", boots_item_0);
        dic.Add("armor_0", armor_item_0);
        dic.Add("armor_1", armor_item_1);

    }
    
    
    private EquippableItem GenerateItem(string itemId, int[] properties, EquipmentType type) {
        EquippableItem item = (EquippableItem)ScriptableObject.CreateInstance("EquippableItem");
        Sprite imageSprite = Resources.Load<Sprite>("loot/" + itemId);
        Sprite imageSpriteSelected = Resources.Load<Sprite>("loot/" + itemId + "Selected");
        item.itemId = itemId;
        item.equipmentType = type;
        item.properties = properties;
        item.icon = imageSprite;
        item.iconSelected = imageSpriteSelected;
        return item;
    }

	public virtual void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Player") {
            FindObjectOfType<AudioManager>().Play("pick_up");
            if (item.equipmentType == EquipmentType.Consume) {
                coll.gameObject.SendMessage("RestoreHealth", item.properties[0]);
                Destroy(gameObject);
                return;
            }
            bool success = FindObjectOfType<Inventory>().AddItem(this.item);
			if(success){
				Destroy(gameObject);
			}
		}
	}

    public virtual void SetItem(string id, float deviation) {
        gameObject.tag = "Loot";
        EquippableItem standard = dic[id];
        gameObject.GetComponent<SpriteRenderer>().sprite = standard.icon;
        float variation = Random.Range(-deviation, deviation);
        int currHp = (int)(standard.properties[0] * (1 + variation));
        int currSpeed = (int)(standard.properties[1] * (1 + variation));
        int currAttack = (int)(standard.properties[2] * (1 + variation));
        int currDefense = (int)(standard.properties[3] * (1 + variation));
        EquippableItem newItem = (EquippableItem)ScriptableObject.CreateInstance("EquippableItem");
        newItem.itemId = id;
        newItem.equipmentType = standard.equipmentType;
        newItem.properties = new int[] { currHp, currSpeed, currAttack, currDefense };
        newItem.icon = standard.icon;
        newItem.iconSelected = standard.iconSelected;
        this.item = newItem;
    }
}
