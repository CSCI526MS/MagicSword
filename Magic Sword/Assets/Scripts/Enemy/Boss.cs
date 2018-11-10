using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {


    float minX;
    float maxX;
    float minY;
    float maxY;

    bool move;

    public BossHealthBar healthBar;
    int health;
	// Use this for initialization
	void Start () {
        initialize();
        minX = GameObject.Find("TopLeft").transform.position.x;
        maxX = GameObject.Find("BottomRight").transform.position.x;
        minY = GameObject.Find("BottomRight").transform.position.y;
        maxY = GameObject.Find("TopLeft").transform.position.y;
        
        //SummonMinions();
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void initialize(){
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

    public void TakeDamage(int damage){
        health -= damage;
        healthBar.Value = health;
        PopupTextController.CreatePopupText(damage.ToString(), transform, Color.white);
        if(health<=0){
            Destroy(gameObject);
        }
    }
}
