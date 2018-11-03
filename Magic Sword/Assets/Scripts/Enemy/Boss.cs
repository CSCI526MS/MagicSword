using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public GameObject flame;
    private readonly float FLAME_DURATION = .5f;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.F)) {
            AoeAttack();
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            gameObject.transform.position = gameObject.transform.position + new Vector3(0, .1f, 0);
        }
	}

    private void AoeAttack()
    {
        var clone = Instantiate(flame, gameObject.transform.position, gameObject.transform.rotation);
        clone.transform.parent = gameObject.transform;
    }
}
