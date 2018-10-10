using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Item : ScriptableObject {

    public string itemId;
	public bool consume;
	public float hp;
	public float speed;
	public float attack;
	public float defense;
    public Sprite icon;
}
