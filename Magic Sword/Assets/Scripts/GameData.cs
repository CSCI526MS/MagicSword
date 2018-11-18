using UnityEngine;

[System.Serializable]
public class GameData
{
    public ItemData[] inventorySlots = new ItemData[20];
    public ItemData[] equipmentSlots = new ItemData[2];
    public Stat playerStatus;
    public Vector3 coordinate;
    public int level;
    public bool[] keyStatus = { false, false, false };
}