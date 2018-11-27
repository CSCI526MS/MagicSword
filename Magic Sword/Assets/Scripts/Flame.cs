using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour {

    private float destroyTime = .5f;

    public LayerMask enemyLayer;

    public LayerMask bossLayer;

    // Use this for initialization
    void Start () {
        CauseDamage();
    }
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, destroyTime);
	}

    private void CauseDamage()
    {
        int attack = 3 * GameObject.Find("Player").GetComponent<Player>().playerStatus.Attack;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 2.8f, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(attack);
        }

        Collider2D[] boss = Physics2D.OverlapCircleAll(transform.position, 2.8f, bossLayer);
        for (int i = 0; i < boss.Length; i++)
        {
            boss[i].GetComponent<Boss>().TakeDamage(attack);
        }
    }
}
