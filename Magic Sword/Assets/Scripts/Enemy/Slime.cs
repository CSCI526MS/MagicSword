using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    protected override void Initialize() {
        health = 60;
        rangedAttackType = false;
    }

}
