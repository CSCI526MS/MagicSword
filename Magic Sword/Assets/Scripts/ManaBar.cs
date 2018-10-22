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
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
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
