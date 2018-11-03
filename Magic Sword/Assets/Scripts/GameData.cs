using UnityEngine;

[System.Serializable]
public class GameData
{
    public static readonly int sizeLimit = 18;
    public ItemData [] itemList = new ItemData[sizeLimit];
    public Stat playerStatus;
    public Vector3 coordinate;
    public int level;
}