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

	private string itemId;
	private static Dictionary<string, Param> dic;
	private Param param;

    static Drops() {
        Debug.Log("Static");
        dic = new Dictionary<string, Param>();
        dic.Add("hp", new Param(true, 10, 0, 0, 0));
        dic.Add("apple", new Param(true, 10, 0, 0, 0));
        dic.Add("mp", new Param(true, 10, 0, 0, 0));
        dic.Add("Meat", new Param(true, 10, 0, 0, 0));
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
			bool success = FindObjectOfType<Inventory>().addItem(itemId);
			if(success){
				Destroy(gameObject);
			}
		}
	}

	public void setItem(string id, float deviation) {
		itemId = id;
		Sprite imageSprite = Resources.Load<Sprite>("RPG_inventory_icons/"+id);
		gameObject.GetComponent<SpriteRenderer>().sprite = imageSprite;
		Param standard = dic[id];
		if (standard.consume) {
			param = new Param(standard.consume, 0, 0, 0, 0);
			return;
		}
		float variation = Random.Range(-deviation, deviation);
		float currHp = standard.hp * (1+variation);
		float currSpeed = standard.speed * (1+variation);
		float currAttack = standard.attack * (1+variation);
		float currDefense = standard.defense * (1+variation);
		param = new Param(standard.consume, currHp, currSpeed, currAttack, currDefense);

	}
}
