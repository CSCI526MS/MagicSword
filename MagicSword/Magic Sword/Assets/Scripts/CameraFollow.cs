using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private readonly float followSpeed = 4.0f;
    private Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player").transform;
        Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = target.position;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition,followSpeed*Time.deltaTime);
    }
}
