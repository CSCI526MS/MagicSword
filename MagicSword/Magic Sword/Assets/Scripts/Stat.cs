using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat {

    [SerializeField]
    private HealthBar hearlthBar;

    private float hpValue;
    private float hpMaxValue;



    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float CurrentHP
    {
        get
        {
            return hpValue;
        }

        set
        {
            hpValue = value;
            hearlthBar.Value = value;
            
        }
    }

    public float MaxHP
    {
        get
        {
            return hpMaxValue;
        }

        set
        {
            hpMaxValue = value;
            hearlthBar.MaxValue = value;
        }
    }

}
