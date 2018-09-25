using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : MonoBehaviour {

	[SerializeField]
	private float destroyTime = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, destroyTime);
	}
}
