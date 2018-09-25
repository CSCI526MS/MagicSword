using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

	// Use this for initialization
	public List<GameObject> slots;
	void Start () {

	}
	void OnEnable()
    {
        List<string> itemList = FindObjectOfType<Inventory>().getItemList();
		for(int i = 0; i < itemList.Count; i += 1){
			Debug.Log(itemList[i]);
			GameObject item = GameObject.Find("ItemImg");
			GameObject newItem = Instantiate(item) as GameObject;
			newItem.transform.position = slots[i].transform.position;
			newItem.transform.SetParent(slots[i].transform);
		}
    }
	// Update is called once per frame
	void Update () {
		
	}
}
