using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {


    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private GameObject player;

    private bool awake;
    private float speed;
    private bool move = true;
    private int moveDirection;

    private int skillSequence = 0;
    private int meteorCounter = 0;
    private bool startMeteorRain; // set "true" to active this skill
    private float lastFireTime;

    private float castInterval = 8;
    private float lastUpdate = 0;



    // skills
    public GameObject fireBall;

    public BossHealthBar healthBar;
    private int health;
	// Use this for initialization
	void Start () {
        initialize();
        startMeteorRain = false;
        lastFireTime = Time.time;
        minX = GameObject.Find("TopLeft").transform.position.x;
        maxX = GameObject.Find("BottomRight").transform.position.x;
        minY = GameObject.Find("BottomRight").transform.position.y;
        maxY = GameObject.Find("TopLeft").transform.position.y;

        player = GameObject.FindGameObjectWithTag("Player");

        //SummonMinions();
    }
	
	// Update is called once per frame
	void Update () {
        if (!awake)
        {
            lastUpdate = Time.time;
        }
        else
        {
            GeneralUpdate();


            if(Time.time - lastUpdate > castInterval)
            {
                move = true;
                CastSkillLoops();
                lastUpdate = Time.time;
            }else if (Time.time - lastUpdate > castInterval - 2)
            {
                //TODO: cast sound
                move = false;

            }

            //startMeteorRain = true;

        }
    }

    private void GeneralUpdate(){
        MeteorRain();
        if(move){
            ChasePlayer();
        }
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) <= 1.5)
        {
            FindObjectOfType<Player>().TakeDamage(30);
        }
    }


    private void initialize(){
        awake = false;
        health = 1000;
        speed = 1;
        healthBar.MaxValue = health;
        moveDirection = 2;
    }

    private void CastSkillLoops(){


        switch(skillSequence){
            case 0:
                FireBall();
                skillSequence++;
                break;
            case 1:
                SummonMinions();
                skillSequence++;
                break;
            case 2:
                startMeteorRain = true;
                skillSequence = 0;
                break;
        }
    }

    private void SummonMinions(){
        for (int i = 0; i < 5;i++){
            GameObject slime = (GameObject)Resources.Load("Prefabs/Enemy/FallingSlime");

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            while(Vector2.Distance(transform.position, new Vector2(randomX, randomY))<2){
                randomX = Random.Range(minX, maxX);
                randomY = Random.Range(minY, maxY);
            }
            GameObject newSlime = Instantiate(slime) as GameObject;
            newSlime.transform.position = new Vector2(randomX, randomY + 20);
        }
    }

    private void MeteorRain(){
        if(startMeteorRain && meteorCounter>=10){
            startMeteorRain = false;
            meteorCounter = 0;
        }
        if(startMeteorRain && Time.time-lastFireTime>0.5){
            FireAMeteor();
            meteorCounter++;
            lastFireTime = Time.time;
        }

    }

    private void FireAMeteor()
    {
        int offset = 5;
        float playerMinX = (player.transform.position.x - offset < minX) ? minX : (player.transform.position.x - offset);
        float playerMaxX = (player.transform.position.x + offset > maxX) ? maxX : (player.transform.position.x + offset);
        float playerMinY = (player.transform.position.y - offset < minY) ? minY : (player.transform.position.y - offset);
        float playerMaxY = (player.transform.position.y + offset > maxY) ? maxY : (player.transform.position.y + offset);

        GameObject meteor = (GameObject)Resources.Load("Prefabs/BossMeteor");

        float randomX = Random.Range(playerMinX, playerMaxX);
        float randomY = Random.Range(playerMinY, playerMaxY);

        meteor = Instantiate(meteor) as GameObject;
        meteor.transform.position = new Vector2(randomX + 20, randomY + 20);
    }

    private void FireBall() {
        float dia = Mathf.Sqrt(2) / 2;
        float velocity = 4f;

        GameObject clone1 = Instantiate(fireBall, gameObject.transform.position, gameObject.transform.rotation);
        GameObject clone2 = Instantiate(fireBall, gameObject.transform.position, gameObject.transform.rotation);
        GameObject clone3 = Instantiate(fireBall, gameObject.transform.position, gameObject.transform.rotation);
        GameObject clone4 = Instantiate(fireBall, gameObject.transform.position, gameObject.transform.rotation);
        GameObject clone5 = Instantiate(fireBall, gameObject.transform.position, gameObject.transform.rotation);
        GameObject clone6 = Instantiate(fireBall, gameObject.transform.position, gameObject.transform.rotation);
        GameObject clone7 = Instantiate(fireBall, gameObject.transform.position, gameObject.transform.rotation);
        GameObject clone8 = Instantiate(fireBall, gameObject.transform.position, gameObject.transform.rotation);
        clone1.transform.rotation = Quaternion.Euler(0f, 0f, 90);
        clone2.transform.rotation = Quaternion.Euler(0f, 0f, 135);
        clone3.transform.rotation = Quaternion.Euler(0f, 0f, 180);
        clone4.transform.rotation = Quaternion.Euler(0f, 0f, 225);
        clone5.transform.rotation = Quaternion.Euler(0f, 0f, 270);
        clone6.transform.rotation = Quaternion.Euler(0f, 0f, 315);
        clone7.transform.rotation = Quaternion.Euler(0f, 0f, 0);
        clone8.transform.rotation = Quaternion.Euler(0f, 0f, 45);
        clone1.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1f) * velocity;
        clone2.GetComponent<Rigidbody2D>().velocity = new Vector2(dia, -dia) * velocity;
        clone3.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0f) * velocity;
        clone4.GetComponent<Rigidbody2D>().velocity = new Vector2(dia, dia) * velocity;
        clone5.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1f) * velocity;
        clone6.GetComponent<Rigidbody2D>().velocity = new Vector2(-dia, dia) * velocity;
        clone7.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 0f) * velocity;
        clone8.GetComponent<Rigidbody2D>().velocity = new Vector2(-dia, -dia) * velocity;
    }

    public void TakeDamage(int damage){
        health -= damage;
        healthBar.Value = health;
        PopupTextController.CreatePopupText(damage.ToString(), transform, Color.white);
        if(health<=0){
            Destroy(gameObject);
        }
    }

    public void Awake()
    {
        awake = true;
    }

    private void ChasePlayer(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Vector2 direction = player.transform.position - transform.position;
        moveDirection = getMoveDirection(direction);
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
}
