using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float speed = 3;
    private Animator animator;

    private Vector2 direction;
  
	// Use this for initialization
	void Start () {
        direction = Vector2.up;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
        Move();

    }

    public void Move(){
        transform.Translate(direction*speed*Time.deltaTime);

        if(direction.x != 0 || direction.y != 0)
        {
            MovementAnimation();
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    private void GetInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }

    private void MovementAnimation()
    {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);

    }
}
