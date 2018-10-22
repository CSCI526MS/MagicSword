using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {

    [SerializeField]
    private Image barContent;
    private float fillAmount;
    private float actualAmount;
    private readonly float waitTime = 10;
    private Vector2 originalPosition;
    private bool shaking = false;
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
    void Start()
    {
        fillAmount = 1;
        actualAmount = 1;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakingTimer > 0)
        {
            shakingTimer -= Time.deltaTime;
        }
        HandleBar();
        if(shaking)
        {
            if (shakingTimer>0)
            {
                transform.position = originalPosition + Random.insideUnitCircle * 3;
            }
            else
            {
                transform.position = originalPosition;
                shaking = false;
            }

        }
        
        
    }

    public void ShakeBar()
    {
        shakingTimer = 0.75f;
        shaking = true;
    }

    private void HandleBar()
    {
        if (barContent.fillAmount < actualAmount)
        {
            barContent.fillAmount += 1.0f / waitTime * Time.deltaTime;
        }
        if (barContent.fillAmount > actualAmount)
        {
            barContent.fillAmount = actualAmount;
        }
    }



}
