using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Param : MonoBehaviour {

	public bool consume;
	public float hp;
	public float speed;
	public float attack;
	public float defense;
	public float deviation;

	public Param (bool consume, 
					float hp, 
					float speed, 
					float attack, 
					float defense) {
		this.consume = consume;
		this.hp = hp;
		this.speed = speed;
		this.attack = attack;
		this.defense = defense;
	}
}
