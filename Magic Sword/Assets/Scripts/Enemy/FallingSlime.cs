using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSlime : Enemy {

    Vector2 targetPosition;
    Collider collider;

    // Use this for initialization
    void Start () {
        targetPosition = new Vector2(transform.position.x, transform.position.y - 20);
        collider = GetComponent<Collider>();
        collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        GeneralUpdate();
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 30 * Time.deltaTime);
        if(collider.enabled == false && Vector2.Distance(transform.position, targetPosition)<1){
            collider.enabled = true;
        }
    }
}
