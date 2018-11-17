using UnityEngine;

public static class GlobalStatic
{
    public static int crossSceneLevel {get; set;}
    public static readonly int inventorySlotNum = 18;
    public static readonly int equipmentSlotNum = 4;
    public static GameObject inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
    public static bool[] keyStatus = { false, false, false };
}