using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Item : ScriptableObject {

    public string itemId;
	public bool consume;
    public float[] properties = new float[4];
    public Sprite icon;
}
