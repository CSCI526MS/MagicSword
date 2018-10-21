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
    private int speed;
    private int attack;
    private int defense;

    public float CurrentHP {
        get {
            return hpValue;
        }
        set {
            hpValue = value;
            hearlthBar.Value = value;
        }
    }

    public float MaxHP {
        get {
            return hpMaxValue;
        }
        set {
            hpMaxValue = value;
            hearlthBar.MaxValue = value;
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

}
