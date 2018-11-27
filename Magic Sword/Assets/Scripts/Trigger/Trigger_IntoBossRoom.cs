using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_IntoBossRoom : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameObject boss = GameObject.Find("Boss");
            boss.GetComponent<Boss>().Awake();
            //GameObject.Find("BossHealthBar").SetActive(true);
            GameObject gate = GameObject.Find("Gate_3");
            gate.GetComponent<SpriteRenderer>().enabled = true;
            gate.GetComponent<BoxCollider2D>().enabled = true;
            Destroy(gameObject);

        }
    }
}
