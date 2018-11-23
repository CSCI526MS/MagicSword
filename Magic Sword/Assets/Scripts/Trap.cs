using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Animator animator;
    private bool attack;
    private float timeStart = 0;
    private bool startTiming = false;
    private bool stab = false;
    private bool playSound = false;
    private List<GameObject> objectsOnTrap = new List<GameObject>();


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
        if (!playSound && !stab && startTiming && Time.time - timeStart > 0.15){
            FindObjectOfType<AudioManager>().Play("trap");
            playSound = true;
        }
        if (!stab && startTiming && Time.time - timeStart > 0.3)
        {
            stab = true;
            foreach (GameObject obj in objectsOnTrap)
            {
                if(obj.tag == "Player"){
                    GameObject.Find("Player").GetComponent<Player>().TakeDamage(5);
                }else if(obj.layer == LayerMask.NameToLayer("Enemy")){
                    obj.GetComponent<Enemy>().TakeDamage(5);
                }


            }
        }
        if (startTiming && Time.time - timeStart > 0.6){
            startTiming = false;
            stab = false;
            playSound = false;
        }
    }



    private void Animation()
    {
        animator.SetBool("attack", attack);

    }


    void OnTriggerEnter2D(Collider2D collider1)
    {
        objectsOnTrap.Add(collider1.gameObject);
      
    }

    void OnTriggerStay2D(Collider2D collider1)
    {
        if(!attack){
            attack = true;
        }

    }

    void OnTriggerExit2D(Collider2D collider1){
        attack = false;
        objectsOnTrap.Remove(collider1.gameObject);

    }



}
