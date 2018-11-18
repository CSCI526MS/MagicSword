using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat {

    public HealthBar healthBar;
    public ManaBar manaBar;

    [SerializeField] public float hpValue;
    [SerializeField] public float hpMaxValue;
    [SerializeField] public int speed;
    [SerializeField] public int attack;
    [SerializeField] public int defense;
    [SerializeField] public float mpValue;
    [SerializeField] public float mpMaxValue;


    public float CurrentHP
    {
        get
        {
            if (hpValue > hpMaxValue)
            {
                hpValue = hpMaxValue;
            }
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
            if (mpValue > mpMaxValue)
            {
                mpValue = mpMaxValue;
            }
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
