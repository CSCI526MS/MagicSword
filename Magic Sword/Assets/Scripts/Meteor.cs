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
    }
}