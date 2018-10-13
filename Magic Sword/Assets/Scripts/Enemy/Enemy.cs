using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected int health;
    private float speed;
    public GameObject drop;
    private float MonsterAttackCooldown;
    private Animator animator;
    private bool move;
    private bool isMonsterAttack;
    private bool MonsterAttackRange;
    private bool MonsterAttack;

    protected bool rangedAttackType;

    // 1 -> Up
    // 2 -> Down
    // 3 -> Left
    // 4 -> Right
    private Transform target;
    private float attackCooldown;

    private readonly float ATTACK_COOLDOWN_TIME = 0.7f;
    private Vector2 direction;
    private int moveDirection;
    private string legendary = "helmets";
    // private string legendary = "eat";
    private int legendaryBar = 75;
    private string epic = "helmets";
    // private string epic = "mp";
    private int epicBar = 50;
    private string rare = "axe";
    // private string rare = "apple";
    private int rareBar = 25;
    private string common = "axe";
    // private string common = "hp";
    private int commonBar = -1;

    private float deviation = 0.1f;

    public GameObject projectile;


    void Start()
    {
        MonsterAttackCooldown = ATTACK_COOLDOWN_TIME;
        isMonsterAttack = true;
        animator = GetComponent<Animator>();
        attackCooldown = ATTACK_COOLDOWN_TIME;
        setHealth();
        MonsterAttack=true;
        speed = 1;
        move = true;
        direction = Vector2.down;
        moveDirection = 2;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        drop = GameObject.Find("Drop");

        setAttackType();
	}


    // Update is called once per frame
    void Update()
    {

        Animation();
        MonsterAttacks();

        direction = target.position - transform.position;
        float distanceSquare = direction.x * direction.x + direction.y * direction.y;
        move = (distanceSquare < 64 && distanceSquare > 2) ? true : false;
        MonsterAttackRange = distanceSquare < 2 ? true : false;
        moveDirection = getMoveDirection(direction);
        if (move)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        MonsterAttack = (MonsterAttackRange)? true : false;

        if (health <= 0)
        {

            dropItems();
            Destroy(gameObject);
        }

        if (!isMonsterAttack)
        {
            attackCooldown -= Time.deltaTime;
        }
        if (attackCooldown < 0)
        {
            isMonsterAttack = true;
        }


    }

    public virtual void setHealth(){
        health = 100;
    }

    protected virtual void setAttackType()
    {

    }


    protected virtual void RangedAttack()
    {



    }


    public void MonsterAttacks(){
        if (isMonsterAttack)
        {
            isMonsterAttack = false;
            attackCooldown = ATTACK_COOLDOWN_TIME;
            if (rangedAttackType)
            {
                RangedAttack();
            }
                
        }

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        PopupTextController.CreatePopupText(damage.ToString(), transform, Color.white);
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
        GameObject loot = (GameObject)Resources.Load("Prefabs/loot");
        loot = Instantiate(loot) as GameObject;
        FindObjectsOfType<Drops>()[0].setItem(id, deviation);
        loot.transform.position = gameObject.transform.position;
    }

    private void Animation()
    {
        animator.SetBool("move", move);
        animator.SetInteger("moveDirection", moveDirection);

        animator.SetBool("MonsterAttack", MonsterAttack);

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
        //if (collision.gameObject.tag == "Skill1")
        //{
        //    Debug.Log("attack.....");
        //    TakeDamage(30);
        //}
    }
}
