using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public List<Item> itemList;
    public Stat playerStatus;
    public Vector3 coordinate;
    public InventorySlot[] inventorySlots;
    public int level;
}