using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy {

    protected override void Initialize()
    {
        health = 100;
        speed = 2;
        attackPoint = 15;
        rangedAttackType = false;
        legendaryBar = 100;
        epicBar = 90;
        rareBar = 60;
        commonBar = 10;
    }
}
