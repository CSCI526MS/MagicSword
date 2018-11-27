using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                string gateName;
                switch(SceneManager.GetActiveScene().name)
                {
                    case "LevelOne": gateName = "Gate_1"; break;
                    case "LevelTwo": gateName = "Gate_2"; break;
                    case "LevelThree": gateName = "Gate_3"; break;
                    default: gateName = ""; break;
                }
                GameObject gate = GameObject.Find(gateName);
                gate.GetComponent<SpriteRenderer>().enabled = false;
                gate.GetComponent<BoxCollider2D>().enabled = false;
                return;
            }
        }
    }
}