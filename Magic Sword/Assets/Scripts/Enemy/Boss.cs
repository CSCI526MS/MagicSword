using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {


    float minX;
    float maxX;
    float minY;
    float maxY;

    bool move;

    // skills
    public GameObject fireBall;

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
        FireBall();
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
}
