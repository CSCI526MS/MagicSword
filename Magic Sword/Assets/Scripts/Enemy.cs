using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private int health;
    private float speed;
    public GameObject drop;

	// Use this for initialization
	void Start () {
        health = 100;
        drop = GameObject.Find("Drop");
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            dropItems();
            Destroy(gameObject);
        }	
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("taken damage!");
    }

    public void dropItems() {
        GameObject newDrop = Instantiate(drop) as GameObject;
		newDrop.transform.position = gameObject.transform.position;
    }
}
