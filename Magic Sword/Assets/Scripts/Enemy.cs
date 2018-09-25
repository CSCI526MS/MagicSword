using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Animator animator;

    private int health;
    private float speed;
    private bool move;

    private Vector2 direction;
    private int moveDirection;
    // 1 -> Up
    // 2 -> Down
    // 3 -> Left
    // 4 -> Right

    private Transform target;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        health = 100;
        speed = 1;
        move = true;
        direction = Vector2.down;
        moveDirection = 2;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Animation();

        direction = target.position - transform.position;
        float distanceSquare = direction.x * direction.x + direction.y * direction.y;
        move = distanceSquare < 64 ? true : false;
        moveDirection = getMoveDirection(direction);

        if (move)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

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

    private void Animation()
    {
        animator.SetBool("move", move);
        animator.SetInteger("moveDirection", moveDirection);
    }

    private int getMoveDirection(Vector2 direction)
    {
        float tan = direction.y / direction.x;
        int moveDirection = 2;
        if (direction.x > 0)
        {
            if (tan <= 1 && tan >= -1)
            {
                // Go right
                moveDirection = 4;
            }
            else if (tan > 1)
            {
                // Go up
                moveDirection = 1;
            }
            else
            {
                // Go down
                moveDirection = 2;
            }
        }
        else
        {
            if (tan <= 1 && tan >= -1)
            {
                // Go left
                moveDirection = 3;
            }
            else if (tan > 1)
            {
                // Go down
                moveDirection = 2;
            }
            else
            {
                // Go up
                moveDirection = 1;
            }
        }

        return moveDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Skill1")
        {
            Debug.Log("attack.....");
            TakeDamage(30);

        }
    }
}