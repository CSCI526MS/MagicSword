using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	// Use this for initialization
	// itemList
	public List<string> itemList = new List<string>();
	private int sizeLimit = 24;
	public bool addItem (string itemId) {
		Debug.Log("item put in the bag");
		if(itemList.Count == sizeLimit) {
			return false;
		}
		itemList.Add(itemId);
		return true;
	}

	public List<string> getItemList() {
		return itemList;
	}
}
