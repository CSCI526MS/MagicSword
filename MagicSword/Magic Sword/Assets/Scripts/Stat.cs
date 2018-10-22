﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat {

    [SerializeField]
    private HealthBar healthBar;

    private float hpValue;
    private float hpMaxValue;
    private int speed;
    private int attack;
    private int defense;

    [SerializeField]
    private ManaBar manaBar;

    private float mpValue;
    private float mpMaxValue;


    public float CurrentHP
    {
        get
        {
            return hpValue;
        }

        set
        {
            if (hpValue > hpMaxValue)
            {
                hpValue = hpMaxValue;
            }
            else
            {
                hpValue = value;
            }
            healthBar.Value = value;
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
            healthBar.MaxValue = value;
        }
    }
    public float CurrentMP
    {
        get
        {
            return mpValue;
        }

        set
        {
            if (mpValue > mpMaxValue)
            {
                mpValue = mpMaxValue;
            }
            else
            {
                mpValue = value;
            }
            manaBar.Value = value;
        }
    }

    public float MaxMP
    {
        get
        {
            return mpMaxValue;
        }

        set
        {
            mpMaxValue = value;
            manaBar.MaxValue = value;
        }
    }

    
    public int Speed {
        get {
            return speed;
        }
        set {
            speed = value;
        }
    }

    public int Attack {
        get {
            return attack;
        }
        set {
            attack = value;
        }
    }

    public int Defense {
        get {
            return defense;
        }
        set {
            defense = value;
        }
    }

    


    public void ShakeBar()
    {
        manaBar.ShakeBar();
    }
}
