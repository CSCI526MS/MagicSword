using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSlime : Enemy {

    Vector2 targetPosition;

    // Use this for initialization
    void Start () {
        targetPosition = new Vector2(transform.position.x, transform.position.y - 20);
	}
	
	// Update is called once per frame
	void Update () {
        GeneralUpdate();
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 30 * Time.deltaTime);
    }
}
