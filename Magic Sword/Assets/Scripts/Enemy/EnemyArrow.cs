using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour {

    private Vector3 normalizeDirection;
    private Vector3 targetPos;

    private bool initialized = false;

    void Start()
    {
        
    }


    void Update()
    {
        if (initialized)
        {
            transform.position += normalizeDirection * 10 * Time.deltaTime;
        }
    }


    public void setPosition(Vector3 target)
    {
        
        targetPos = target;
        normalizeDirection = (targetPos - transform.position).normalized;
        initialized = true;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

            GameObject.Find("Player").GetComponent<Player>().TakeDamage(5);
        }
        FindObjectOfType<AudioManager>().Play("arrow_hit");
        Destroy(gameObject);
    }
}
