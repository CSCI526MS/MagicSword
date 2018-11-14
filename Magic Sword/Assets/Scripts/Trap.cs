using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Animator animator;
    private bool attack;
    private float timeStart = 0;
    private bool startTiming = false;



    void Start()
    {
        attack = false;
        animator = GetComponent<Animator>();
       
    }


    // Update is called once per frame
    void Update()
    {
        Animation();
        if(attack && !startTiming){
            startTiming = true;
            timeStart = Time.time;
        }
        if(startTiming && Time.time - timeStart > 0.2)
        {
            GameObject.Find("Player").GetComponent<Player>().TakeDamage(5);
            startTiming = false;
        }
    }



    private void Animation()
    {
        animator.SetBool("attack", attack);

    }


    void OnTriggerStay2D(Collider2D collider1)
    {
        if(collider1.gameObject.tag == "Player"){
            attack = true;
        }
      
    }
    void OnTriggerExit2D(Collider2D collider1){
        attack = false;
        startTiming = false;

    }



}
