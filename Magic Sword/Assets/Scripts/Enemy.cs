using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private int health;
    private float speed;

	// Use this for initialization
	void Start () {
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);
        }	
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("taken damage!");
    }
}
