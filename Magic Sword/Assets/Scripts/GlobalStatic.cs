using UnityEngine;

public static class GlobalStatic
{
    public static int crossSceneLevel {get; set;}
    public static readonly int inventorySlotNum = 18;
    public static readonly int equipmentSlotNum = 4;
    public static GameObject inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
    public static GameObject canvas = GameObject.FindGameObjectWithTag("MainUICanvas");
    public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static GameObject background;
    public static bool[] keyStatus = { false, false, false };
}