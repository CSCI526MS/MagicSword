using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private int health;
    private float speed;
    public GameObject drop;

    private Animator animator;
    private bool move;
    // 1 -> Up
    // 2 -> Down
    // 3 -> Left
    // 4 -> Right
    private Transform target;

    private Vector2 direction;
    private int moveDirection;
    private string legendary = "Meat";
    private int legendaryBar = 75;
    private string epic = "mp";
    private int epicBar = 50;
    private string rare = "apple";
    private int rareBar = 25;
    private string common = "hp";
    private int commonBar = -1;

    private float deviation = 0.1f;

	void Start () {
        animator = GetComponent<Animator>();
        health = 100;
        speed = 1;
        move = true;
        direction = Vector2.down;
        moveDirection = 2;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        drop = GameObject.Find("Drop");
	}
	
	// Update is called once per frame
	void Update () {
        Animation();

        direction = target.position - transform.position;
        float distanceSquare = direction.x*direction.x + direction.y*direction.y;
        move = distanceSquare<64 ? true : false;
        moveDirection = getMoveDirection(direction);

        if (move) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
        if (health <= 0)
        {
            dropItems();
            Destroy(gameObject);
        }
	}

    public void TakeDamage(int damage) {
        health -= damage;
        Debug.Log("taken damage!");
    }

    public void dropItems() {
        int random = Random.Range(0, 100);
        Debug.Log("random" + random);
        string id;
        if (random<commonBar) {
            return;
        } else if (random<rareBar) {
            id = common;
        } else if (random<epicBar) {
            id = rare;
        } else if (random<legendaryBar) {
            id = epic;
        } else {
            id = legendary;
        }
        Debug.Log("id"+id);
        GameObject newDrop = Instantiate(drop) as GameObject;
        FindObjectsOfType<Drops>()[0].setItem(id, deviation);
        newDrop.transform.position = gameObject.transform.position;
    }

    private void Animation() {
        animator.SetBool("move", move);
        animator.SetInteger("moveDirection", moveDirection);
    }

     private int getMoveDirection(Vector2 direction) {
        float tan = direction.y / direction.x;
        int moveDirection = 2;
        if(direction.x > 0) {
            if(tan <= 1 && tan >= -1) {
                // Go right
                moveDirection = 4;
            } else if(tan > 1) {
                // Go up
                moveDirection = 1;
            } else {
                // Go down
                moveDirection = 2;
            }
        } else {
            if (tan <= 1 && tan >= -1) {
                // Go left
                moveDirection = 3;
            } else if (tan > 1) {
                // Go down
                moveDirection = 2;
            } else {
                // Go up
                moveDirection = 1;
            }
        }

        return moveDirection;
    }
}
