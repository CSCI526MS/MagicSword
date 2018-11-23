using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    protected int health;
    protected float speed;
    protected int attackPoint;
    private float MonsterAttackCooldown;
    private Animator animator;
    private bool move;
    protected bool isAttack;
    protected bool awake;
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
    private string[] legendary = new string[] { "weapon_2", "helmet_1", "armor_1" };
    // private string legendary = "eat";
    public int legendaryBar;
    private string[] epic = new string[] { "weapon_1", "helmet_0", "boots_0", "armor_0" };
    // private string epic = "mp";
    public int epicBar;
    private string[] rare = new string[] { "weapon_0" };
    // private string rare = "apple";
    public int rareBar;
    private string[] common = new string[] { "hp_potion_0", "hp_potion_1", "hp_potion_2" };
    // private string common = "hp";
    public int commonBar;
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
        GeneralStart();

    }

    protected void GeneralStart(){
        sRenderer = GetComponent<SpriteRenderer>();
        MonsterAttackCooldown = ATTACK_COOLDOWN_TIME;
        isAttack = false;
        animator = GetComponent<Animator>();
        attackCooldown = 0;
        Initialize();
        move = true;
        direction = Vector2.down;
        moveDirection = 2;
        awake = true;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        lastSpot = transform.position;
        aware = false;
    }


    // Update is called once per frame
    void Update() {
        GeneralUpdate();

    }

    protected void GeneralUpdate(){
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
            if (!isAttack && Vector2.Distance(transform.position, lastSpot) > 0.5)
            {
                // if it is an archer
                if (rangedAttackType && !Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Wall")).collider && Vector2.Distance(transform.position, player.position) < 8)
                {
                    moveDirection = GetMoveDirection(direction);
                    // if the enemy get too close to player -> run away
                    if (!isAttack && Vector2.Distance(transform.position, player.position) < 6)
                    {
                        Vector3 target = new Vector2(2 * transform.position.x - lastSpot.x, 2 * transform.position.y - lastSpot.y);
                        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                        direction = target - transform.position;
                        moveDirection = GetMoveDirection(direction);
                    }
                    else
                    {
                        Wander();
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, lastSpot, speed * Time.deltaTime);
                    direction = lastSpot - transform.position;
                    moveDirection = GetMoveDirection(direction);
                }
            }
            else
            {
                aware = false;
                Wander();
            }
            if (health <= 0)
            {
                Debug.Log("dead");
                DropItems();
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
        //attackPoint =
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
    }

    private void Wander()
    {
        if (!isAttack)
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



    }

    private void MeleeAttack()
    {
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) <= 1.5)
        {   
            FindObjectOfType<AudioManager>().Play("slime_bite");
            isAttack = true;
            attackCooldown = ATTACK_COOLDOWN_TIME;
            FindObjectOfType<Player>().TakeDamage(attackPoint);
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

    public void DropItems()
    {
        int random = Random.Range(0, 100);
        Debug.Log("random" + random);
        string id;
        int level;
      
        switch (SceneManager.GetActiveScene().name)
        {
            case "LevelOne": id = "key_0"; level = 0; break;
            case "LevelTwo": id = "key_1"; level = 1;   break;
            case "LevelThree": id = "key_2"; level = 2; break;
            default: id = "key_0"; level = 0; break;
        }
        
        if (GameObject.FindGameObjectsWithTag("Slime").Length <= 5 && GlobalStatic.keyStatus[level] == false)
        {

            GlobalStatic.keyStatus[level] = true;
            GameObject key = (GameObject)Resources.Load("Prefabs/key");
            key = Instantiate(key) as GameObject;
            FindObjectsOfType<Key>()[0].SetItem(id, 0);
            key.transform.position = gameObject.transform.position;
        }
        else
        {
            if (random < commonBar)
            {
                return;
            }
            else if (random < rareBar)
            {
                int ind = (int)Random.Range(0, (float)(common.Length - 0.000001));
                id = common[ind];
            }
            else if (random < epicBar)
            {
                int ind = (int)Random.Range(0, (float)(rare.Length - 0.000001));
                id = rare[ind];
            }
            else if (random < legendaryBar)
            {
                int ind = (int)Random.Range(0, (float)(epic.Length - 0.000001));
                id = epic[ind];
            }
            else
            {
                int ind = (int)Random.Range(0, (float)(legendary.Length - 0.000001));
                id = legendary[ind];
            }
            GameObject loot = (GameObject)Resources.Load("Prefabs/loot");
            loot = Instantiate(loot) as GameObject;
            FindObjectsOfType<Drops>()[0].SetItem(id, deviation);
            loot.transform.position = gameObject.transform.position;
        }
    }

    private void Animation() {
        animator.SetBool("move", move);
        animator.SetInteger("moveDirection", moveDirection);
        animator.SetInteger("attackDirection", attackDirection);

        animator.SetBool("MonsterAttack", isAttack);

    }

    protected int GetMoveDirection(Vector2 direction) {
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

    public void SetAwake(bool b)
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
