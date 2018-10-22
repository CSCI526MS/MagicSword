using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    // Next update in second
    private int nextUpdate = 1;

    private SpriteRenderer sRenderer;
    [SerializeField]

    private static readonly int DEFAULT_SPEED = 10;

    private float healthRegeneration;
    private float manaRegeneration;
    private Animator animator;
    private FixedJoystick joystick;
    private bool isMove;
    private bool isAttack;
    private bool isImmune;

    private readonly int SKILL1_MANA_COST = 5;

    [SerializeField]
    private Stat playerStatus;

    // moveDirection == 1 -> Up
    // moveDirection == 2 -> Down
    // moveDirection == 3 -> Left
    // moveDirection == 4 -> Right
    private int moveDirection;
    private float tan;

    private Vector2 direction;
    private Vector2 touchDirection;

    private readonly float ATTACK_COOLDOWN_TIME = 0.7f;
    private readonly float IMMUNE_TIME = 2f;
    private int speed;
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

    public GameObject meteor;

    // thunderball
    public GameObject thunderBall;

    // Use this for initialization
    void Start () {
        Initialize();
        sRenderer = GetComponent<SpriteRenderer>();
        PopupTextController.Initialize();
        joystick = FindObjectOfType<FixedJoystick>();
        Debug.Log(joystick);
        animator = GetComponent<Animator>();
        direction = Vector2.down;
        isMove = false;
        moveDirection = 2;
        attackCooldown = ATTACK_COOLDOWN_TIME;
        isImmune = false;
        SceneManager.sceneLoaded += (var, var2) =>
        {
            if (var.buildIndex == 0)
            {
                transform.position = new Vector3(0, 0, 0);
            }
            else if (var.buildIndex == 1)
            {
                transform.position = new Vector3(0, 0, 0);
            }
        };
    }

    private void Initialize()
    {
        Debug.Log("ln 95");
        playerStatus.Speed = DEFAULT_SPEED;
        playerStatus.Attack = 10;
        playerStatus.Defense = 0;
        Debug.Log("ln 98");
        playerStatus.CurrentHP = 100;
        playerStatus.MaxHP = 100;
        playerStatus.CurrentMP = 100;
        playerStatus.MaxMP = 100;
        healthRegeneration = 0;
        manaRegeneration = 3;
        Debug.Log("ln 104");
    }

	// Update is called once per frame
	void Update () {
        Debug.Log("222");
        direction = joystick.Direction;
        Debug.Log("333");
        Animation();
        Move();
        Attack();
        AttackDirection();
        if (isAttack) {
            speed = 0;
        } else {
            speed = playerStatus.Speed;
        }

        if (immuneTimer < 0 && isImmune) {
            isImmune = false;

            // turn on renderer in case the renderer is disabled at the last frame of flash.
            sRenderer.enabled = true;
        }

        if (isImmune) {
            FlashSprite();
        }

        if (immuneTimer > 0) {
            immuneTimer -= Time.deltaTime;
        }

        // MeteorAttack();

        if (Input.GetMouseButtonDown(0) && !joystick.isTouched()) {
            //Debug.Log(Input.mousePosition);
            //touchDirection = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            //touchDirection.Normalize();
            //Debug.Log(touchDirection);
            Vector3 castPoint;
            castPoint = Input.mousePosition;
            castPoint.z = 0.0f;
            Vector3 shootDirection = Camera.main.ScreenToWorldPoint(castPoint);
            shootDirection = shootDirection - transform.position;
            touchDirection = shootDirection;
            touchDirection.Normalize();
            if (!isAttack)
            {
                if(playerStatus.CurrentMP >= SKILL1_MANA_COST)
                {
                    RemoteAttack();
                    DirectionUpdate(new Vector2(castPoint.x - Screen.width / 2, castPoint.y - Screen.height / 2));
                    isAttack = true;
                }
                else
                {
                    OutOfMana();
                }
                
            }

            
        }
        //if (Input.touchCount > 0)
        //{
        //    int numOfTouches = Input.touches.Length;
        //    if (Input.touches[numOfTouches - 1].phase == TouchPhase.Ended)
        //    {
        //        Vector3 shootDirection;
        //        shootDirection = Input.GetTouch(numOfTouches - 1).position;
        //        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        //        shootDirection = shootDirection - transform.position;
        //        touchDirection = shootDirection;
        //        touchDirection.Normalize();
        //        RemoteAttack();
        //    }
        //}
        
        

    }

    private void LateUpdate()
    {
        if (Time.time >= nextUpdate)
        {
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;

            Regeneration();
        }
    }

    private void Regeneration()
    {
        playerStatus.CurrentHP += healthRegeneration;
        playerStatus.CurrentMP += manaRegeneration;
    }

    private void Move(){
        transform.Translate(direction*speed*Time.deltaTime);
        if ((direction.x != 0 || direction.y != 0) && !isAttack) {
            isMove = true;
        }
        else {
            isMove = false;
        }

        DirectionUpdate(direction);

    }

    public int getPlayerDamage() {
        return playerStatus.Attack;
    }

    private void DirectionUpdate(Vector2 direction) {
        tan = direction.y / direction.x;
        if (direction.x > 0) {
            if (tan <= 1 && tan >= -1) {
                // Go right
                moveDirection = 4;
            }
            if (tan > 1) {
                // Go up
                moveDirection = 1;
            }
            if (tan < -1) {
                // Go down
                moveDirection = 2;
            }


        }
        else if (direction.x < 0) {
            if (tan <= 1 && tan >= -1) {
                // Go left
                moveDirection = 3;
            }
            if (tan > 1) {
                // Go down
                moveDirection = 2;
            }
            if (tan < -1) {
                // Go up
                moveDirection = 1;
            }
        }
    }

    private void Improve(int[] properties) {
        playerStatus.MaxHP += properties[0];
        playerStatus.Speed += properties[1];
        playerStatus.Attack += properties[2];
        playerStatus.Defense += properties[3];
        Debug.Log("Improve: MaxHp:"+playerStatus.MaxHP+" Hp:"+ playerStatus.CurrentHP+" Speed:"+playerStatus.Speed+" Attack:"+playerStatus.Attack+" Defense:"+playerStatus.Defense);
    }

    private void Decline(int[] properties) {
        playerStatus.MaxHP -= properties[0];
        if (playerStatus.CurrentHP > playerStatus.MaxHP) {
            playerStatus.CurrentHP = playerStatus.MaxHP;
        }
        playerStatus.Speed -= properties[1];
        playerStatus.Attack -= properties[2];
        playerStatus.Defense -= properties[3];
        Debug.Log("Decline: MaxHp:"+playerStatus.MaxHP+" Hp:"+ playerStatus.CurrentHP+" Speed:"+playerStatus.Speed+" Attack:"+playerStatus.Attack+" Defense:"+playerStatus.Defense);
    }

    private void Attack() {
        if (isAttack) {
            attackCooldown -= Time.deltaTime;
            if(attackCooldown < 0) {
                isAttack = false;
                attackCooldown = ATTACK_COOLDOWN_TIME;
            }
        }
    }

    private void AttackDirection() {
        switch (moveDirection) {
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

    void OnCollisionStay2D(Collision2D collision) {
        //if (collision.gameObject.tag == "Enemy" && !isImmune) {
        //    isImmune = true;
        //    immuneTimer = IMMUNE_TIME;
        //    TakeDamage(10);
        //}
        //if (collision.gameObject.tag == "Slime" && !isImmune) {
        //    isImmune = true;
        //    immuneTimer = IMMUNE_TIME;
        //    TakeDamage(5);
        //}
        if (collision.gameObject.tag == "Portal") {
            SceneManager.LoadScene("LevelTwo");
        }
    }

    private void Dash() {
        // this.speed = 30;
    }

    public void MeleeAttack() {

        if (!isAttack) {
            isAttack = true;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, 9);
            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<Enemy>().TakeDamage(playerStatus.Attack);
            }

            // Camera shake effect
            Vector3 deltaPosition = Vector3.zero;
            camera.transform.localPosition -= deltaPosition;
            deltaPosition = Random.insideUnitCircle * 0.5f;
            camera.transform.position += deltaPosition;
        }
    }

    public void RemoteAttack()
    {
        playerStatus.CurrentMP -= SKILL1_MANA_COST;
        //touchDirection = new Vector2();
        var clone = Instantiate(thunderBall, gameObject.transform.position + new Vector3(touchDirection.x, touchDirection.y, 0), gameObject.transform.rotation);
        //clone.velocity = direction * 10;

        float degree = (float)((Mathf.Atan2(touchDirection.x, touchDirection.y) / Mathf.PI) * 180f);
        if (degree < -90) {
            degree = -degree - 90;
        }
        else if (degree > 0 && degree < 90)
        {
            degree = -(degree + 90);
        }
        else if (degree > 90)
        {
            degree = 90 + (190 - degree);
        }
        else
        {
            degree = -90 - degree;
        }

        Vector3 temp = clone.transform.eulerAngles;
        temp.z = degree;
        clone.transform.eulerAngles = temp;
        clone.GetComponent<Rigidbody2D>().velocity = touchDirection * 10f;

    }

    public void RestoreHealth(int health) {
        playerStatus.CurrentHP += health;
        if (playerStatus.CurrentHP > playerStatus.MaxHP) {
            playerStatus.CurrentHP = playerStatus.MaxHP;
        }
        PopupTextController.CreatePopupText(health.ToString(), transform, Color.green);
    }

    public void RestoreMana(int mana)
    {
        playerStatus.CurrentMP += mana;
        if (playerStatus.CurrentMP > playerStatus.MaxMP)
        {
            playerStatus.CurrentMP = playerStatus.MaxMP;
        }
        PopupTextController.CreatePopupText(mana.ToString(), transform, Color.blue);
    }

    public void TakeDamage(int damage) {
        if (!isImmune) {
            immuneTimer = IMMUNE_TIME;
            damage = (int)(damage * (0.2+20/(float)(playerStatus.Defense+25)));
            playerStatus.CurrentHP -= damage;
            PopupTextController.CreatePopupText(damage.ToString(), transform, Color.red);
            isImmune = true;
        }
        
    }

    private void FlashSprite() {
        if (flashTimer > 0.05) {
            flashTimer = 0;
            toggle = !toggle;
            if (toggle) {
                sRenderer.enabled = true;
            }
            else {
                sRenderer.enabled = false;
            }
        }
        else {
            flashTimer += Time.deltaTime;
        }
    }

    private void Animation() {
        animator.SetBool("move", isMove);
        animator.SetInteger("moveDirection", moveDirection);
        animator.SetBool("attack", isAttack);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void MeteorAttack() {

        if (Input.GetMouseButtonDown(0) && !joystick.isTouched()) {
            if (!isAttack) {
                Vector3 touchPoint;
                touchPoint = Input.mousePosition;
                touchPoint.z = 0.0f;
                Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(touchPoint), Color.red, 3);
                Vector2 castPoint;
                RaycastHit2D barrier = Physics2D.Linecast(transform.position, Camera.main.ScreenToWorldPoint(touchPoint), 1 << LayerMask.NameToLayer("Wall"));

                if (barrier.collider) {// if there is a barrier between player and cast point;
                    castPoint = Camera.main.WorldToScreenPoint(barrier.point);
                }
                else{
                    castPoint = touchPoint;
                }
                DirectionUpdate(new Vector2(castPoint.x - Screen.width / 2, castPoint.y - Screen.height / 2));
                castPoint = Camera.main.ScreenToWorldPoint(castPoint);
                isAttack = true;

                GameObject newMeteor = Instantiate(meteor) as GameObject;
                FindObjectOfType<Meteor>().Create(castPoint);
                newMeteor.transform.position = new Vector3(castPoint.x + 15, castPoint.y + 15, 0);
            }

        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OutOfMana()
    {
        playerStatus.ShakeBar();
    }
}
