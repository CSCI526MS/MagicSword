using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float speed = 10;
    private Animator animator;
    private FixedJoystick joystick;
    private bool isMove;

    // moveDirection == 1 -> Up
    // moveDirection == 2 -> Down
    // moveDirection == 3 -> Left
    // moveDirection == 4 -> Right
    private int moveDirection;

    private float sin;
    

    private Vector2 direction;
  
	// Use this for initialization
	void Start () {
        joystick = FindObjectOfType<FixedJoystick>();
        direction = Vector2.up;
        animator = GetComponent<Animator>();
        isMove = false;
        moveDirection = 2;

    }
	
	// Update is called once per frame
	void Update () {
        direction = joystick.Direction;
        Move();

    }

    public void Move(){
        transform.Translate(direction*speed*Time.deltaTime);
        MovementAnimation();
        if (direction.x != 0 || direction.y != 0)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        sin = direction.y / direction.x;
        if(direction.x > 0)
        {
            if(sin <= 1 && sin >= -1)
            {
                // Go right 
                moveDirection = 4;
            }
            if(sin > 1)
            {
                // Go up
                moveDirection = 1;
            }
            if(sin < -1)
            {
                // Go down
                moveDirection = 2;
            }
            
            
        }
        else if(direction.x < 0)
        {
            if (sin <= 1 && sin >= -1)
            {
                // Go left 
                moveDirection = 3;
            }
            if (sin > 1)
            {
                // Go down
                moveDirection = 2;
            }
            if (sin < -1)
            {
                // Go up
                moveDirection = 1;
            }
        }

    }


    private void MovementAnimation()
    {
        animator.SetBool("move", isMove);
        animator.SetInteger("moveDirection", moveDirection);
    }
}
