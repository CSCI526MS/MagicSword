using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Item : ScriptableObject {

    public string itemId;
	public bool consume;
    public int[] properties = new int[4];
    public Sprite icon;
}
