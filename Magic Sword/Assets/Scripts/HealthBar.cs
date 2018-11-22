using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    private Image barContent;
    private float fillAmount;
    private float actualAmount;
    private readonly float waitTime = 10;
    private Vector2 originalPosition;
    private float shakingTimer;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            actualAmount = value / MaxValue;
        }
    }


    // Use this for initialization
    void Start ()
    {
        fillAmount = 1;
        actualAmount = 1;
        originalPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        HandleBar();
        if (barContent.fillAmount < 0.2)
        {
            if((int)Time.time % 2 == 1)
            {
                transform.position = originalPosition + Random.insideUnitCircle * 5;
            }
            else
            {
                transform.position = originalPosition;
            }
        }
    }

    protected void HandleBar()
    {
        if(barContent.fillAmount < actualAmount)
        {
            //barContent.fillAmount += 1.0f / waitTime * Time.deltaTime;
            barContent.fillAmount = actualAmount;
        }
        if (barContent.fillAmount > actualAmount)
        {
            barContent.fillAmount = actualAmount;
        }

    }


}
