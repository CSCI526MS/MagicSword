using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Enemy {

    protected override void Initialize()
    {
        health = 80;
        speed = 3;
        attackPoint = 10;
        rangedAttackType = false;
        legendaryBar = 100;
        epicBar = 90;
        rareBar = 60;
        commonBar = 30;
    }
}
