using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    public override void setHealth() {
        health = 10;
    }

    protected override void setAttackType()
    {
        rangedAttackType = false;
    }
}
