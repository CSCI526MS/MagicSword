using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drops : MonoBehaviour {

	/*
	Catagory == -1: Undefined
	Catagoty == 0: Armor
	Catagory == 1: Weapon
	Catagory == 2: Shoes
	Catagory == 3: Potion
	*/

   	private static Dictionary<string, Item> dic;
    public EquippableItem item;

    /*
    static Drops() {
        Debug.Log("Static");
        dic = new Dictionary<string, Item>();
        Item item = new Item();
        item.itemId =
        dic.Add("hp", new Item("hp", true, 10, 0, 0, 0, null));
        dic.Add("apple", new Item("apple", true, 10, 0, 0, 0, null));
        dic.Add("mp", new Item("mp", true, 10, 0, 0, 0,null));
        dic.Add("meat", new Item("meat", true, 10, 0, 0, 0,null));
    }*/
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
			bool success = FindObjectOfType<Inventory>().addItem(this.item);
			if(success){
				Destroy(gameObject);
			}
		}
	}

	public void setItem(string id, float deviation) {
        Sprite imageSprite = Resources.Load<Sprite>("RPG_inventory_icons/"+id);
		gameObject.GetComponent<SpriteRenderer>().sprite = imageSprite;
		// Item standard = ScriptableObject.CreateInstance("Item") as Item;
        EquippableItem standard = ScriptableObject.CreateInstance("EquippableItem") as EquippableItem;
        if (id == "axe") {
            standard.EquipmentType = EquipmentType.Weapon;
        } else if (id == "helmets") {
            standard.EquipmentType = EquipmentType.Helmet;
        }
        Debug.Log(id + " is  " + standard.EquipmentType);

        if (standard.consume) {
            this.item = ScriptableObject.CreateInstance("EquippableItem") as EquippableItem;
            // Debug.Log(this.item.EquipmentType);
            itemModifier(id, standard.consume, 0, 0, 0, 0, imageSprite);
			return;
		}
		float variation = Random.Range(-deviation, deviation);
		float currHp = standard.hp * (1+variation);
		float currSpeed = standard.speed * (1+variation);
		float currAttack = standard.attack * (1+variation);
		float currDefense = standard.defense * (1+variation);
        this.item = ScriptableObject.CreateInstance("EquippableItem") as EquippableItem;
        if (id == "axe") {
            this.item.EquipmentType = EquipmentType.Weapon;
        } else if (id == "helmets") {
            this.item.EquipmentType = EquipmentType.Helmet;
        }
        itemModifier(id, standard.consume, currHp, currSpeed, currAttack, currDefense, imageSprite);

	}

    private void itemModifier(string itemId, bool consume, float hp, float speed, float attack, float defense, Sprite icon) {
        this.item.itemId = itemId;
        this.item.consume = consume;
        this.item.hp = hp;
        this.item.speed = speed;
        this.item.attack = attack;
        this.item.defense = defense;
        this.item.icon = icon;
    }
}
