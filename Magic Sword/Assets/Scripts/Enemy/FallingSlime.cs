using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSlime : Enemy {

    private Vector2 targetPosition;
    private CircleCollider2D collider;
    private bool inPosition = false;

    // Use this for initialization
    void Start () {
        GeneralStart();
        awake = false;
        targetPosition = new Vector2(transform.position.x, transform.position.y - 20);
        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        GeneralUpdate();
        if(!inPosition){
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 30 * Time.deltaTime);
        }
        if(!inPosition && Vector2.Distance(transform.position, targetPosition)<1){
            inPosition = true;
            collider.enabled = true;
            awake = true;
        }

    }

    protected override void Initialize()
    {
        health = 60;
        rangedAttackType = false;
    }
}
