using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drops : MonoBehaviour {


   	private static Dictionary<string, EquippableItem> dic;
    public EquippableItem item;

    public Drops() {

    }

    static Drops() {
        Debug.Log("Static");
        dic = new Dictionary<string, EquippableItem>(); 
        dic.Add("hp", new EquippableItem("hp", EquipmentType.Consume, 10, 0, 0, 0, null));
        dic.Add("axe", new EquippableItem("axe", EquipmentType.Weapon, 0, 0, 10, 0, null));
        dic.Add("helmets", new EquippableItem("helmets", EquipmentType.Helmet, 0, 0, 0, 10, null));
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
                coll.gameObject.SendMessage("RestoreHealth", item.hp);
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
        this.item = ScriptableObject.CreateInstance("EquippableItem") as EquippableItem;

        this.item.itemId = id;
        if (id == "axe") {
            this.item.properties[0] = 100;
            this.item.properties[3] = 30;
            // this.item.hp = 1000;
            this.item.EquipmentType = EquipmentType.Weapon;

        } else if (id == "helmets") {
            this.item.properties[0] = 70;
            this.item.properties[2] = 60;
            this.item.EquipmentType = EquipmentType.Helmet;
        }
        this.item.icon = imageSprite;
        EquippableItem standard = dic[id];

		float variation = Random.Range(-deviation, deviation);
		float currHp = standard.hp * (1+variation);
		float currSpeed = standard.speed * (1+variation);
		float currAttack = standard.attack * (1+variation);
		float currDefense = standard.defense * (1+variation);
        this.item = new EquippableItem(id, standard.EquipmentType, currHp, currSpeed, currAttack, currDefense, imageSprite);
	}
}
