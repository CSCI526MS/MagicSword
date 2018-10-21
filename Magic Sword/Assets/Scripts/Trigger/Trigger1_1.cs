using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1_1 : MonoBehaviour {

    private GameObject goblinSniper;


    private void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            goblinSniper = (GameObject)Resources.Load("Prefabs/Enemy/GoblinSniper");
            EnemySpawnEvent();
            Destroy(gameObject);
        }
    }

    private void EnemySpawnEvent()
    {
        GameObject goblinSniper1 = Instantiate(goblinSniper) as GameObject;
        goblinSniper1.transform.position = GameObject.Find("Spawn1-1-1").transform.position;
        goblinSniper1.GetComponent<Enemy>().ImmuneTrigger();

        GameObject goblinSniper2 = Instantiate(goblinSniper) as GameObject;
        goblinSniper2.transform.position = GameObject.Find("Spawn1-1-2").transform.position;
        goblinSniper2.GetComponent<Enemy>().ImmuneTrigger();

        GameObject goblinSniper3 = Instantiate(goblinSniper) as GameObject;
        goblinSniper3.transform.position = GameObject.Find("Spawn1-1-3").transform.position;
        goblinSniper3.GetComponent<Enemy>().ImmuneTrigger();

        GameObject goblinSniper4 = Instantiate(goblinSniper) as GameObject;
        goblinSniper4.transform.position = GameObject.Find("Spawn1-1-4").transform.position;
        goblinSniper4.GetComponent<Enemy>().ImmuneTrigger();
    }

}
