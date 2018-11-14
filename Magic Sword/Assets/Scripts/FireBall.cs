using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    private float destroyTime = .5f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(GameObject.Find("Player").GetComponent<Player>().getPlayerDamage());
        }

        if (collision.gameObject.name == "Boss")
        {
            GameObject.Find("Boss").GetComponent<Boss>().TakeDamage(GameObject.Find("Player").GetComponent<Player>().getPlayerDamage());
        }

        FindObjectOfType<AudioManager>().Play("fire_hit");
        Destroy(gameObject);
        
    }
}
