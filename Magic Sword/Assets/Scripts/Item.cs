using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject {

    public string itemId;
    //hp speed attack defense
    public int[] properties = new int[4];
    public Sprite icon;
    public Sprite iconSelected;
}
