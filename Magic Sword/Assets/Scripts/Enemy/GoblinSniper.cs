using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSniper : Enemy {

    protected override void Initialize()
    {
        health = 100;
        rangedAttackType = true;
        legendaryBar = 90;
        epicBar = 80;
        rareBar = 40;
        commonBar = 10;
    }



    protected override void RangedAttack()
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        RaycastHit2D barrier = Physics2D.Linecast(transform.position, playerPosition, 1 << LayerMask.NameToLayer("Wall"));

        if (!barrier && Vector2.Distance(transform.position, playerPosition)<8 && aware)
        {
            Vector3 direction = playerPosition - transform.position;
            attackDirection = GetMoveDirection(direction);
            isAttack = true;

            attackCooldown = ATTACK_COOLDOWN_TIME;
            GameObject missile = Instantiate(projectile, transform.position, transform.rotation);
            Vector3 diff = playerPosition - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            missile.transform.rotation = Quaternion.Euler(0f, 0f, rot_z );
            FindObjectOfType<EnemyArrow>().setPosition(playerPosition);
            //missile.GetComponent<EnemyFireBall>().setPosition(playerPosition);

            
        }


    }
}
