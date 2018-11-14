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
        EquippableItem hp_potion_0 = generateItem("hp_potion_0", new int[]{10, 0, 0, 0}, EquipmentType.Consume);
        EquippableItem hp_potion_1 = generateItem("hp_potion_1", new int[]{30, 0, 0, 0}, EquipmentType.Consume);
        EquippableItem hp_potion_2 = generateItem("hp_potion_2", new int[]{70, 0, 0, 0}, EquipmentType.Consume);
        
        // weapon
        EquippableItem weapon_item_0 = generateItem("weapon_0", new int[]{0, 0, 10, 0}, EquipmentType.Weapon);
        EquippableItem weapon_item_1 = generateItem("weapon_1", new int[]{0, 0, 30, 0}, EquipmentType.Weapon);
        EquippableItem weapon_item_2 = generateItem("weapon_2", new int[]{0, 0, 70, 0}, EquipmentType.Weapon);
        
        // helmets
        EquippableItem helmet_item_0 = generateItem("helmet_0", new int[]{0, 0, 0, 10}, EquipmentType.Helmet);
        EquippableItem helmet_item_1 = generateItem("helmet_1", new int[]{0, 0, 0, 30}, EquipmentType.Helmet);

        // boots
        EquippableItem boots_item_0 = generateItem("boots_0", new int[]{0, 10, 0, 0}, EquipmentType.Boots);

        // armor
        EquippableItem armor_item_0 = generateItem("armor_0", new int[]{0, 0, 0, 10}, EquipmentType.Armor);
        EquippableItem armor_item_1 = generateItem("armor_1", new int[]{0, 0, 0, 30}, EquipmentType.Armor);

        EquippableItem ringsItem = (EquippableItem)ScriptableObject.CreateInstance("EquippableItem");
        ringsItem.itemId = "rings";
        ringsItem.equipmentType = EquipmentType.Ring;
        ringsItem.properties = new int[] { 0, 10, 0, 0 };
        ringsItem.icon = null;

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
        dic.Add("rings", ringsItem);

    }
    
    
    private EquippableItem generateItem(string name, int[] properties, EquipmentType type) {
        EquippableItem item = (EquippableItem)ScriptableObject.CreateInstance("EquippableItem");
        
        item.itemId = name;
        item.equipmentType = type;
        item.properties = properties;
        item.icon = null;
        
        return item;
    }

    // Use this for initialization

    void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("Slime");
        Debug.Log("count" + cubes.Length);

    }

    // Update is called once per frame
    void Update () {
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Player") {
			/*
				TODO:
				Find the Inventory Object
				Add this drop to inventory by calling Inventory.add(itemId)
				pass the itemId to add method
			*/
            FindObjectOfType<AudioManager>().Play("pick_up");
            if (item.equipmentType == EquipmentType.Consume) {
                coll.gameObject.SendMessage("RestoreHealth", item.properties[0]);
                Destroy(gameObject);
                return;
            }
            if (item.equipmentType ==EquipmentType.Ring)
            {
                Debug.Log("drop72");
                coll.gameObject.SendMessage("Getkey", item.properties[1]);
                Destroy(gameObject);
                var cylinder = GameObject.Find("door");
                Destroy(cylinder);
                return;
            }

            bool success = FindObjectOfType<Inventory>().addItem(this.item);
			if(success){
				Destroy(gameObject);
			}
		}
	}

	public void SetItem(string id, float deviation) {
        gameObject.tag = "Loot";
        Sprite imageSprite = Resources.Load<Sprite>("loot/"+id);
        Sprite imageSpriteSelected = Resources.Load<Sprite>("loot/"+id + "Selected");
		gameObject.GetComponent<SpriteRenderer>().sprite = imageSprite;
        EquippableItem standard = dic[id];

		float variation = Random.Range(-deviation, deviation);
        int currHp = (int)(standard.properties[0] * (1 + variation));
        int currSpeed = (int)(standard.properties[1] * (1 + variation));
        int currAttack = (int)(standard.properties[2] * (1 + variation));
        int currDefense = (int)(standard.properties[3] * (1 + variation));
        EquippableItem newItem = (EquippableItem)ScriptableObject.CreateInstance("EquippableItem");
        newItem.itemId = id;
        newItem.equipmentType = standard.equipmentType;
        newItem.properties = new int[] { currHp, currSpeed, currAttack, currDefense };
        newItem.icon = imageSprite;
        newItem.iconSelected = imageSpriteSelected;
        this.item = newItem;
        //this.item = new EquippableItem(id, standard.EquipmentType, currHp, currSpeed, currAttack, currDefense, imageSprite);
    }
}
