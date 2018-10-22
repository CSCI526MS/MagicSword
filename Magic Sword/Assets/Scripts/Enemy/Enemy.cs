using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    protected int health;
    private float speed;
    private float MonsterAttackCooldown;
    private Animator animator;
    private bool move;
    protected bool isAttack;
    private bool awake;
    private bool isImmune = false;
    private SpriteRenderer sRenderer;

    protected bool rangedAttackType;

    // 1 -> Up
    // 2 -> Down
    // 3 -> Left
    // 4 -> Right
    private Transform player;
    protected float attackCooldown;

    protected readonly float ATTACK_COOLDOWN_TIME = 1f;
    private Vector2 direction;
    protected int moveDirection;
    protected int attackDirection;
    private string legendary = "helmets";
    // private string legendary = "eat";
    private int legendaryBar = 75;
    private string epic = "axe";
    // private string epic = "mp";
    private int epicBar = 50;
    private string rare = "hp";
    // private string rare = "apple";
    private int rareBar = 25;
    private string common = "hp";
    // private string common = "hp";
    private int commonBar = -1;

    private float deviation = 0.1f;

    public GameObject projectile;

    private float wanderTimer;
    private Vector3 lastSpot;
    protected bool aware;

    // for sprite flash
    bool toggle = true;
    float flashTimer = 0;
    float immuneTimer = 1;

    void Start() {
        sRenderer = GetComponent<SpriteRenderer>();
        MonsterAttackCooldown = ATTACK_COOLDOWN_TIME;
        isAttack = false;
        animator = GetComponent<Animator>();
        attackCooldown = 0;
        Initialize();
        speed = 1;
        move = true;
        direction = Vector2.down;
        moveDirection = 2;
        awake = true;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //drop = GameObject.Find("Drop");
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        new Drops(); // call static constructor

        lastSpot = transform.position;
        aware = false;

    }


    // Update is called once per frame
    void Update() {

        if (awake)
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
            //Debug.Log(lastSpot);


            // if enemy not aware and player is in sight
            if (!aware && Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < 10 && !Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Wall")).collider)
            {
                aware = true;
                lastSpot = player.position;
            }

            // update player's last seen spot
            if (aware && !Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Wall")).collider)
            {
                lastSpot = player.position;
            }

            // move towards last spot
            if (Vector2.Distance(transform.position, lastSpot) > 0.5)
            {
                // if it is an archer
                if(rangedAttackType && !Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Wall")).collider && Vector2.Distance(transform.position, player.position) < 8)
                {
                    moveDirection = getMoveDirection(direction);
                    // if the enemy get too close to player -> run away
                    if(!isAttack && Vector2.Distance(transform.position, player.position) < 6)
                    {
                        Vector3 target = new Vector2(2 * transform.position.x - lastSpot.x, 2 * transform.position.y - lastSpot.y);
                        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                        direction = target - transform.position;
                        moveDirection = getMoveDirection(direction);
                        
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, lastSpot, speed * Time.deltaTime);
                    direction = lastSpot - transform.position;
                    moveDirection = getMoveDirection(direction);
                }
               
            }
            else
            {
                aware = false;
                Wander();
            }


            if (health <= 0)
            {
                dropItems();
                Destroy(gameObject);
            }

            if (attackCooldown >= 0)
            {
                attackCooldown -= Time.deltaTime;
            }
            if (attackCooldown < ATTACK_COOLDOWN_TIME - 0.3)
            {
                isAttack = false;
            }

            if (wanderTimer >= 0)
            {
                wanderTimer -= Time.deltaTime;
            }



          
        }

        if (immuneTimer < 0 && isImmune)
        {
            isImmune = false;
            awake = true;
            // turn on renderer in case the renderer is disabled at the last frame of flash.
            sRenderer.enabled = true;
        }

        if (isImmune)
        {
            awake = false;
            FlashSprite();
        }

        if (immuneTimer > 0)
        {
            immuneTimer -= Time.deltaTime;
        }



    }

    protected virtual void Initialize(){
        //health = 
        //speed = 
        //TODO: damage = 
        //rangedAttackType =
    }



    protected virtual void RangedAttack() {


    }


    private void CreateText(string msg, Color color, int fontSize, Transform parent,float x, float y, float time)
    {
        GameObject UItext = (GameObject)Resources.Load("Prefabs/UI/Text");
        UItext.transform.SetParent(parent);

        UItext.GetComponent<Text>().text = msg;
        UItext.GetComponent<Text>().fontSize = fontSize;
        UItext.GetComponent<Text>().color = color;

        //Destroy(UItext, time);
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

    public void TakeDamage(int damage) {
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

        GameObject loot = (GameObject)Resources.Load("Prefabs/loot");
        loot = Instantiate(loot) as GameObject;
        FindObjectsOfType<Drops>()[0].setItem(id, deviation);
        loot.transform.position = gameObject.transform.position;
    }

    private void Animation() {
        animator.SetBool("move", move);
        animator.SetInteger("moveDirection", moveDirection);
        animator.SetInteger("attackDirection", attackDirection);

        animator.SetBool("MonsterAttack", isAttack);

    }

    protected int getMoveDirection(Vector2 direction) {
        float tan = direction.y / direction.x;
        int moveDirection = 2;
        if (direction.x > 0) {
            if (tan <= 1 && tan >= -1) {
                // Go right
                moveDirection = 4;
            }
            else if (tan > 1) {
                // Go up
                moveDirection = 1;
            }
            else {
                // Go down
                moveDirection = 2;
            }
        }
        else {
            if (tan <= 1 && tan >= -1) {
                // Go left
                moveDirection = 3;
            }
            else if (tan > 1) {
                // Go down
                moveDirection = 2;
            }
            else {
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

    public void setAwake(bool b)
    {
        awake = b;
    }

    public void ImmuneTrigger()
    {
        isImmune = true;
        immuneTimer = 1;
    }

    private void FlashSprite()
    {
        if (flashTimer > 0.1)
        {
            flashTimer = 0;
            toggle = !toggle;
            if (toggle)
            {
                sRenderer.enabled = true;
            }
            else
            {
                sRenderer.enabled = false;
            }
        }
        else
        {
            flashTimer += Time.deltaTime;
        }
        Debug.Log("FlashSprite()");
    }
}
