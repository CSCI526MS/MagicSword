using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour {

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
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    protected void HandleBar()
    {

        if (barContent.fillAmount < actualAmount)
        {
            barContent.fillAmount += 2f / waitTime * Time.deltaTime;
        }
        if (barContent.fillAmount > actualAmount)
        {
            barContent.fillAmount -= 2f / waitTime * Time.deltaTime;
        }

    }

}
