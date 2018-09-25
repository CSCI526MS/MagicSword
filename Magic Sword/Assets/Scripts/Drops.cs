using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {

	/*
	Catagory == -1: Undefined
	Catagoty == 0: Armor
	Catagory == 1: Weapon
	Catagory == 2: Shoes
	Catagory == 3: Potion
	*/
	string itemId = "000000";
	// Use this for initialization
	void Start () {
		//set image according to itemId
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Player")
		{
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
}
