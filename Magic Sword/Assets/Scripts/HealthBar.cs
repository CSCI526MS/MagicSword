using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    private Image barContent;
    private float fillAmount;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            fillAmount = value / MaxValue;
        }
    }


    // Use this for initialization
    void Start () {
        fillAmount = 1;

    }
	
	// Update is called once per frame
	void Update () {
        HandleBar();
    }

    private void HandleBar()
    {
        if(barContent.fillAmount!= this.fillAmount)
        {
            barContent.fillAmount = this.fillAmount;
        }

    }


}
