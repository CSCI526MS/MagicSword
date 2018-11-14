using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemData {
    public string itemId;
    public int[] properties = new int[4];
    public EquipmentType equipmentType;
    public Sprite icon;
    public Sprite iconSelected;
}