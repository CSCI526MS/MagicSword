using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    private float speed;
    public Vector3 targetPosition;
    private bool initialized = false;

    private GameObject fireExplosion;
    //private GameObject smallFires;

    public LayerMask enemyLayer; 

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
            }
        }
        

    }

    public void Create(Vector3 target)
    {
        targetPosition = target;
        initialized = true;
        
    }

    private void ExplosionEffect()
    {
        fireExplosion = (GameObject) Resources.Load("Prefabs/FireExplosion");
        GameObject newExplosion = Instantiate(fireExplosion) as GameObject;
        newExplosion.transform.position = transform.position;
        //smallFires = (GameObject)Resources.Load("Prefabs/SmallFires");
        //GameObject newSmallFires = Instantiate(smallFires) as GameObject;
        //newSmallFires.transform.position = transform.position;
        //Destroy(newSmallFires, 5);
    }

    private void CauseDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 1, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(100);
        }
    }
}