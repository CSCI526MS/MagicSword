using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    private float speed;
    public Vector3 targetPosition;
    private bool initialized = false;

    private GameObject circle;
    private GameObject fireExplosion;
    //private GameObject smallFires;

    public LayerMask enemyLayer;
    public LayerMask bossLayer;

    void Start()
    {
        speed = 20.0f;
        
    }

    void Update()
    {
        if (initialized)
        {
            

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if(transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
            {
                Destroy(gameObject);
                ExplosionEffect();
                CauseDamage();
                Destroy(circle);
            }
        }
        

    }

    public void Create(Vector3 target)
    {
        targetPosition = target;
        initialized = true;
        circle = (GameObject)Resources.Load("Prefabs/Circle");
        circle = Instantiate(circle) as GameObject;
        circle.transform.position = target;

    }

    private void ExplosionEffect()
    {
        fireExplosion = (GameObject) Resources.Load("Prefabs/FireExplosion");
        fireExplosion = Instantiate(fireExplosion) as GameObject;
        fireExplosion.transform.position = transform.position;
        //smallFires = (GameObject)Resources.Load("Prefabs/SmallFires");
        //GameObject newSmallFires = Instantiate(smallFires) as GameObject;
        //newSmallFires.transform.position = transform.position;
        //Destroy(newSmallFires, 5);
    }

    private void CauseDamage()
    {
        int attack = 5 * GameObject.Find("Player").GetComponent<Player>().playerStatus.Attack;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 1, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(attack);
        }

        Collider2D[] boss = Physics2D.OverlapCircleAll(transform.position, 1, bossLayer);
        for (int i = 0; i < boss.Length; i++)
        {
            boss[i].GetComponent<Boss>().TakeDamage(attack);
        }

    }
}