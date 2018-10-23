using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drops : MonoBehaviour {


   	private static Dictionary<string, EquippableItem> dic;
    public EquippableItem item;

    void Awake() {
        dic = new Dictionary<string, EquippableItem>();
        EquippableItem hpItem = (EquippableItem)ScriptableObject.CreateInstance("EquippableItem");
        hpItem.itemId = "hp";
        hpItem.EquipmentType = EquipmentType.Consume;
        hpItem.properties = new int[] { 10, 0, 0, 0 };
        hpItem.icon = null;
        EquippableItem axeItem = (EquippableItem)ScriptableObject.CreateInstance("EquippableItem");
        axeItem.itemId = "axe";
        axeItem.EquipmentType = EquipmentType.Weapon;
        axeItem.properties = new int[] { 0, 0, 10, 0 };
        axeItem.icon = null;
        EquippableItem helmetsItem = (EquippableItem)ScriptableObject.CreateInstance("EquippableItem");
        helmetsItem.itemId = "helmets";
        helmetsItem.EquipmentType = EquipmentType.Helmet;
        helmetsItem.properties = new int[] { 0, 0, 0, 10 };
        helmetsItem.icon = null;
        dic.Add("hp", hpItem);
        dic.Add("axe", axeItem);
        dic.Add("helmets", helmetsItem);
    }

	// Use this for initialization

	void Start () {
		//set image according to itemId
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
            if (item.EquipmentType == EquipmentType.Consume) {
                coll.gameObject.SendMessage("RestoreHealth", item.properties[0]);
                Destroy(gameObject);
                return;
            }
			bool success = FindObjectOfType<Inventory>().addItem(this.item);
			if(success){
				Destroy(gameObject);
			}
		}
	}

	public void setItem(string id, float deviation) {
        gameObject.tag = "Loot";
        Sprite imageSprite = Resources.Load<Sprite>("RPG_inventory_icons/"+id);
		gameObject.GetComponent<SpriteRenderer>().sprite = imageSprite;
        EquippableItem standard = dic[id];

		float variation = Random.Range(-deviation, deviation);
        int currHp = (int)(standard.properties[0] * (1 + variation));
        int currSpeed = (int)(standard.properties[1] * (1 + variation));
        int currAttack = (int)(standard.properties[2] * (1 + variation));
        int currDefense = (int)(standard.properties[3] * (1 + variation));
        EquippableItem newItem = (EquippableItem)ScriptableObject.CreateInstance("EquippableItem");
        newItem.itemId = id;
        newItem.EquipmentType = standard.EquipmentType;
        newItem.properties = new int[] { currHp, currSpeed, currAttack, currDefense };
        newItem.icon = imageSprite;
        this.item = newItem;
        //this.item = new EquippableItem(id, standard.EquipmentType, currHp, currSpeed, currAttack, currDefense, imageSprite);
    }
}
