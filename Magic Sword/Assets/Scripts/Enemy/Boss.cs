using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {


    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private Transform player;

    private bool awake;
    private bool move;

    private int meteorCounter;
    private bool startMeteorRain; // set "true" to active this skill
    private float lastFireTime;

    private float lastUpdate = 0;



    public BossHealthBar healthBar;
    private int health;
	// Use this for initialization
	void Start () {
        initialize();
        meteorCounter = 0;
        startMeteorRain = false;
        lastFireTime = Time.time;
        minX = GameObject.Find("TopLeft").transform.position.x;
        maxX = GameObject.Find("BottomRight").transform.position.x;
        minY = GameObject.Find("BottomRight").transform.position.y;
        maxY = GameObject.Find("TopLeft").transform.position.y;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //SummonMinions();
    }
	
	// Update is called once per frame
	void Update () {
        if (awake)
        {
            GeneralUpdate();
            //if(Time.time- lastUpdate > 3){
            //    SummonMinions();
            //    lastUpdate = Time.time;
            //}

            //startMeteorRain = true;

        }
    }

    private void GeneralUpdate(){
        MeteorRain();
    }


    private void initialize(){
        awake = false;
        health = 1000;
        healthBar.MaxValue = health;
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

    private void FireAMeteor(){
        int offset = 5;
        float playerMinX = (player.position.x - offset < minX) ? minX : (player.position.x - offset);
        float playerMaxX = (player.position.x + offset > maxX) ? maxX : (player.position.x + offset);
        float playerMinY = (player.position.y - offset < minY) ? minY : (player.position.y - offset);
        float playerMaxY = (player.position.y + offset > maxY) ? maxY : (player.position.y + offset);

        GameObject meteor = (GameObject)Resources.Load("Prefabs/BossMeteor");

        float randomX = Random.Range(playerMinX, playerMaxX);
        float randomY = Random.Range(playerMinY, playerMaxY);

        meteor = Instantiate(meteor) as GameObject;
        meteor.transform.position = new Vector2(randomX + 20, randomY + 20);

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
}
