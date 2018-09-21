using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour {

    [SerializeField]
    private Animator animator;


	// Use this for initialization
	void Start () {
        int randomNumber = Random.Range(0, 3);
        animator.SetInteger("random", randomNumber);
        //AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //Destroy(gameObject, clipInfo[0].clip.length);

        // !!! Destroy time is related to the duration of animation. Refactory of random animation probably is needed in the future.
        Destroy(gameObject, 0.5f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateText(string newText, Color newColor)
    {
        animator.GetComponent<Text>().text = newText;
        animator.GetComponent<Text>().color = newColor;
    }


}
