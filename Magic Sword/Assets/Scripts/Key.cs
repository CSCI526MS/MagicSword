using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Drops {
    public override void SetItem(string id, float deviation)
    {
        gameObject.tag = "Loot";
        EquippableItem newItem = (EquippableItem)ScriptableObject.CreateInstance("EquippableItem");
        Sprite imageSprite = Resources.Load<Sprite>("loot/" + id);
        gameObject.GetComponent<SpriteRenderer>().sprite = imageSprite;
        newItem.itemId = id;
        newItem.equipmentType = EquipmentType.Key;
        newItem.properties = new int[] {0,0,0,0};
        newItem.icon = imageSprite;
        newItem.iconSelected = null;
        this.item = newItem;
    }

    public override void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            FindObjectOfType<AudioManager>().Play("pick_up");
            if (item.equipmentType == EquipmentType.Key)
            {
                coll.gameObject.SendMessage("Getkey");
                Destroy(gameObject);
                GameObject gate = GameObject.Find("Gate");
                gate.GetComponent<SpriteRenderer>().enabled = false;
                gate.GetComponent<BoxCollider2D>().enabled = false;
                return;
            }
        }
    }
}