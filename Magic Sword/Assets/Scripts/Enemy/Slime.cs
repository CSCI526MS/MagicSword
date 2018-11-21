using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    protected override void Initialize() {
        health = 60;
        speed = 1;
        attackPoint = 10;
        rangedAttackType = false;
        legendaryBar = 100;
        epicBar = 90;
        rareBar = 60;
        commonBar = 30;
    }

}
