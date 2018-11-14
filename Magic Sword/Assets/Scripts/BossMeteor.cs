using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeteor : MonoBehaviour {

    Vector2 targetPosition;
    private GameObject circle;
    private GameObject fireExplosion;

    public LayerMask playerLayer;

    // Use this for initialization
    void Start () {
        targetPosition = new Vector2(transform.position.x-20, transform.position.y - 20);
        circle = (GameObject)Resources.Load("Prefabs/BossCircle");
        circle = Instantiate(circle) as GameObject;
        circle.transform.position = targetPosition;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 30 * Time.deltaTime);
        if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
        {
            Destroy(gameObject);
            ExplosionEffect();
            CauseDamage();
            Destroy(circle);
        }


    }

    private void ExplosionEffect(){
        fireExplosion = (GameObject)Resources.Load("Prefabs/FireExplosion");
        fireExplosion = Instantiate(fireExplosion) as GameObject;
        fireExplosion.transform.position = transform.position;
    }

    private void CauseDamage(){
        Collider2D player = Physics2D.OverlapCircle(transform.position, 1, playerLayer);
        if(player){
            player.GetComponent<Player>().TakeDamage(20);
        }

    }
}
