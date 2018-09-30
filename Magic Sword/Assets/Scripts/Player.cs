using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {
    Touch touch;
    private SpriteRenderer sRenderer;
    [SerializeField]
    private readonly float DEFAULT_SPEED = 10;
    private float speed;
    private Animator animator;
    private FixedJoystick joystick;
    private bool isMove;
    private bool isAttack;
    private bool isImmune;
    

    [SerializeField]
    private Stat playerStatus;


    public ParticleSystem FlashEffect;

    // moveDirection == 1 -> Up
    // moveDirection == 2 -> Down
    // moveDirection == 3 -> Left
    // moveDirection == 4 -> Right
    private int moveDirection;
    private float tan;
    

    private Vector2 direction;

    private readonly float ATTACK_COOLDOWN_TIME = 0.7f;
    private readonly float IMMUNE_TIME = 2f;
    private float attackCooldown;
    private float immuneTimer = 0;
    
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayer;
    private Vector2 attackPosUp = new Vector2(0, 0.1f);
    private Vector2 attackPosDown = new Vector2(0, -0.1f);
    private Vector2 attackPosRight = new Vector2(0.1f, 0);
    private Vector2 attackPosLeft = new Vector2(-0.1f, 0);

    public Camera camera;

    // for sprite flash (while immune)
    float flashTimer = 0;
    bool toggle = true;

    private Vector3 touchDirection;

    private Rect noTouchArea = new Rect(0, (int)(Screen.height*0.66), (int)(Screen.width*0.25), (int)(Screen.height*0.33));


    public GameObject meteor;

    // Use this for initialization
    void Start () {
        sRenderer = GetComponent<SpriteRenderer>();
        PopupTextController.Initialize();
        FlashEffect.Stop();
        joystick = FindObjectOfType<FixedJoystick>();
        direction = Vector2.down;
        speed = DEFAULT_SPEED;
        animator = GetComponent<Animator>();
        isMove = false;
        moveDirection = 2;
        attackCooldown = ATTACK_COOLDOWN_TIME;
        playerStatus.MaxHP = 100;
        playerStatus.CurrentHP = 100;
        isImmune = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        touch = Input.GetTouch(0);
        direction = joystick.Direction;
        Animation();
        Move();
        Attack();
        AttackDirection();
        if (isAttack)
        {
            speed = 0;
        }
        else
        {
            speed = DEFAULT_SPEED;
        }

        if (immuneTimer<0 && isImmune)
        {
            isImmune = false;

            // turn on renderer in case the renderer is disabled at the last frame of flash.
            sRenderer.enabled = true;
        }

        if (isImmune)
        {
            FlashSprite();
        }

        if (immuneTimer > 0)
        {
            immuneTimer -= Time.deltaTime;
        }

        MeteorAttack();
        
    }

    private void Move(){
        transform.Translate(direction*speed*Time.deltaTime);
        if ((direction.x != 0 || direction.y != 0) && !isAttack)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        DirectionUpdate(direction);

    }

    private void DirectionUpdate(Vector2 direction)
    {
        tan = direction.y / direction.x;
        if (direction.x > 0)
        {
            if (tan <= 1 && tan >= -1)
            {
                // Go right 
                moveDirection = 4;
            }
            if (tan > 1)
            {
                // Go up
                moveDirection = 1;
            }
            if (tan < -1)
            {
                // Go down
                moveDirection = 2;
            }


        }
        else if (direction.x < 0)
        {
            if (tan <= 1 && tan >= -1)
            {
                // Go left 
                moveDirection = 3;
            }
            if (tan > 1)
            {
                // Go down
                moveDirection = 2;
            }
            if (tan < -1)
            {
                // Go up
                moveDirection = 1;
            }
        }
    }

    private void Attack()
    {
        
        if (isAttack)
        {
            attackCooldown -= Time.deltaTime;
            if(attackCooldown < 0)
            {
                isAttack = false;
                attackCooldown = ATTACK_COOLDOWN_TIME;
            }
        }
    }

    private void AttackDirection()
    {
        switch (moveDirection)
        {
            case 1:
                attackPos.localPosition = attackPosUp;
                break;
            case 2:
                attackPos.localPosition = attackPosDown;
                break;
            case 3:
                attackPos.localPosition = attackPosLeft;
                break;
            case 4:
                attackPos.localPosition = attackPosRight;
                break;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isImmune) {
            isImmune = true;
            immuneTimer = IMMUNE_TIME;
            TakeDamage(10);
        }
if (collision.gameObject.tag == "floor") {
StartCoroutine(LoadYourAsyncScene());
}

    }

    private void Dash() {
        // this.speed = 30;
    }

    public void MeleeAttack() {
        //transform.Translate(direction*10);
        //if (FlashEffect.isPlaying) {
        //    FlashEffect.Stop();
        //} else {
        //    FlashEffect.Play();
        //}

        if (!isAttack)
        {
            isAttack = true;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, 9);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().TakeDamage(30);
            }

            // Camera shake effect
            Vector3 deltaPosition = Vector3.zero;
            camera.transform.localPosition -= deltaPosition;
            deltaPosition = Random.insideUnitCircle * 0.5f;
            camera.transform.position += deltaPosition;

        }
    }

    public void TakeDamage(int damage)
    {

        playerStatus.CurrentHP -= damage;
        PopupTextController.CreatePopupText(damage.ToString(), transform, Color.red);

        
    }

    private void FlashSprite()
    {
        if (flashTimer > 0.05)
        {
            flashTimer = 0;
            toggle = !toggle;
            if (toggle)
            {
                sRenderer.enabled = true;
            }
            else
            {
                sRenderer.enabled = false;
            }
        }
        else
        {
            flashTimer += Time.deltaTime;
        }
    }
IEnumerator LoadYourAsyncScene()
{


AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelOne");

while (!asyncLoad.isDone)
{
yield return null;
}
}
    private void Animation()
    {
        animator.SetBool("move", isMove);
        animator.SetInteger("moveDirection", moveDirection);
        animator.SetBool("attack", isAttack);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void MeteorAttack()
    {
        
        if (!joystick.isTouched())
        {
            if (!isAttack)
            {

                Vector3 touchPoint = touch.position;
                touchPoint.z = 0.0f;
                if (!noTouchArea.Contains(touchPoint))
                {
                    touchPoint = Camera.main.ScreenToWorldPoint(touchPoint);

                    DirectionUpdate(touchPoint);
                    isAttack = true;

                    GameObject newMeteor = Instantiate(meteor) as GameObject;
                    FindObjectOfType<Meteor>().Create(touchPoint);
                    newMeteor.transform.position = new Vector3(touchPoint.x + 15, touchPoint.y + 15, 0);
                }
                

                
                
            }
            
        }
    }

}





