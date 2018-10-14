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
    protected bool isAttack;

    protected bool rangedAttackType;

    // 1 -> Up
    // 2 -> Down
    // 3 -> Left
    // 4 -> Right
    private Transform player;
    protected float attackCooldown;

    protected readonly float ATTACK_COOLDOWN_TIME = 1f;
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

    private float wanderTimer;
    private Vector3 lastSpot;
    protected bool aware;
    private bool lastAwareStatus;

    void Start()
    {
        MonsterAttackCooldown = ATTACK_COOLDOWN_TIME;
        isAttack = false;
        animator = GetComponent<Animator>();
        attackCooldown = 0;
        setHealth();
        speed = 1;
        move = true;
        direction = Vector2.down;
        moveDirection = 2;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        drop = GameObject.Find("Drop");

        setAttackType();
        lastSpot = transform.position;
        aware = false;
        lastAwareStatus = false;

    }


    // Update is called once per frame
    void Update()
    {

        Animation();
        MonsterAttacks();

        //direction = target.position - transform.position;
        //float distanceSquare = direction.x * direction.x + direction.y * direction.y;
        //move = (distanceSquare < 64 && distanceSquare > 2) ? true : false;

        //moveDirection = getMoveDirection(direction);
        //if (move)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //}
        Debug.Log(lastSpot);

        if (!aware && Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < 5 && !Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Wall")).collider)
        {
            aware = true;
            AwareSign(true);
            lastSpot = player.position;
        }

        if(aware && !Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Wall")).collider)
        {
            lastSpot = player.position;
        }

        if (Vector2.Distance(transform.position, lastSpot) > 0) 
        {
            transform.position = Vector2.MoveTowards(transform.position, lastSpot, speed * Time.deltaTime);
            direction = lastSpot - transform.position;
            moveDirection = getMoveDirection(direction);
        }
        else
        {
            aware = false;
            AwareSign(false);
            Wander();
        }


        if (health <= 0)
        {

            dropItems();
            Destroy(gameObject);
        }

        if (attackCooldown>=0)
        {
            attackCooldown -= Time.deltaTime;
        }
        if (attackCooldown < 0.5)
        {
            isAttack = false;
        }

        if (wanderTimer >= 0)
        {
            wanderTimer -= Time.deltaTime;
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

    private void AwareSign(bool aware)
    {
        if(lastAwareStatus !=aware)
        {
            if (aware)
            {
                
            }
            else
            {
               
            }
        }
        lastAwareStatus = aware;
        

    }

    private void Wander()
    {
        if (wanderTimer > 0)
        {
            switch (moveDirection)
            {
                case 1:
                    transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                    break;
                case 2:
                    transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                    break;
                case 3:
                    transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                    break;
                case 4:
                    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                    break;
            }
        }
        else
        {
            wanderTimer = Random.Range(1, 3);
            moveDirection = Random.Range(1, 5);
        }


    }

    private void MeleeAttack()
    {
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) <= 1.5)
        {
            isAttack = true;
            attackCooldown = ATTACK_COOLDOWN_TIME;
            FindObjectOfType<Player>().TakeDamage(10);
        }
        
    }


    public void MonsterAttacks(){
        if (!isAttack && attackCooldown <= 0)
        {
            
            if (rangedAttackType)
            {
                RangedAttack();
            }
            else
            {
                MeleeAttack();
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

        animator.SetBool("MonsterAttack", isAttack);

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
