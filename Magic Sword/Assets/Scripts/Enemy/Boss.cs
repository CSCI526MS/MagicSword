using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {


    float minX;
    float maxX;
    float minY;
    float maxY;

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
        health = 100;
        healthBar.MaxValue = health;
    }

    private void SummonMinions(){
        //TODO: disable collision when falling
        //TODO: exclude BOSS position for spawn place
        for (int i = 0; i < 5;i++){
            GameObject slime = (GameObject)Resources.Load("Prefabs/Enemy/FallingSlime");

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            GameObject newSlime = Instantiate(slime) as GameObject;
            newSlime.transform.position = new Vector2(randomX, randomY + 20);
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        healthBar.Value = health;
        PopupTextController.CreatePopupText(damage.ToString(), transform, Color.white);
    }
}
