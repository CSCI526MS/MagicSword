using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Animator animator;
    private bool attack;



    void Start()
    {
        attack = false;
        animator = GetComponent<Animator>();
       
    }


    // Update is called once per frame
    void Update()
    {
        Animation();
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

    }



}
