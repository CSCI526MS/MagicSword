using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour {

    private float destroyTime = .5f;

    public LayerMask enemyLayer;

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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 2.8f, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(60);
            Debug.Log(enemies[i]);
        }
    }
}
