using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class Player : MonoBehaviour {

    // Next update in second
    private int nextUpdate = 1;

    private SpriteRenderer sRenderer;
    [SerializeField]

    private float healthRegeneration;
    private float manaRegeneration;
    private Animator animator;
    private FixedJoystick joystick;
    private AudioSource footstepSound;
    private SkillCoolDown FireBallButton;
    private SkillCoolDown FlameButton;
    private SkillCoolDown MeteorButton;
    private bool isMove;
    private bool isAttack;
    private bool isCoolDown;
    private bool isImmune;
    private bool invincible = false;

    [SerializeField]
    public Stat playerStatus;

    // moveDirection == 1 -> Up
    // moveDirection == 2 -> Down
    // moveDirection == 3 -> Left
    // moveDirection == 4 -> Right
    private int moveDirection;
    private float tan;

    private Vector2 direction;
    private Vector2 touchDirection;

    private readonly float ATTACK_COOLDOWN_TIME = 0.7f;
    private float skillCoolDownTime = 1f;
    private readonly float IMMUNE_TIME = 2f;
    private int speed;
    private float attackCooldown;
    private float skillCoolDown;
    private float immuneTimer = 0;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayer;
    public Vector3 initPosition = new Vector3(0, 0, 0);
    private Vector2 attackPosUp = new Vector2(0, 0.1f);
    private Vector2 attackPosDown = new Vector2(0, -0.1f);
    private Vector2 attackPosRight = new Vector2(0.1f, 0);
    private Vector2 attackPosLeft = new Vector2(-0.1f, 0);

    public Camera camera;
    public Animator transition;
    private GameObject transitionPanel;

    // for sprite flash (while immune)
    float flashTimer = 0;
    bool toggle = true;

    // Skill
    enum CurrentSkill { FireBall, Meteor, Flame };
    private CurrentSkill currentSkill;
    private Skill skill;
    private readonly int SKILL1_MANA_COST = 5;
    private readonly int SKILL2_MANA_COST = 20;
    private readonly int SKILL3_MANA_COST = 15;

    // meteor
    public GameObject meteor;

    // fireball
    public GameObject fireBall;

    // flame
    public GameObject flame;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        Initialize();
        sRenderer = GetComponent<SpriteRenderer>();
        PopupTextController.Initialize();
        joystick = FindObjectOfType<FixedJoystick>();
        FireBallButton = GameObject.Find("FireBallCooldown").GetComponent<SkillCoolDown>();
        FlameButton = GameObject.Find("FlameCooldown").GetComponent<SkillCoolDown>();
        MeteorButton = GameObject.Find("MeteorCooldown").GetComponent<SkillCoolDown>();
        animator = GetComponent<Animator>();
        direction = Vector2.down;
        isMove = false;
        isCoolDown = true;
        moveDirection = 2;
        attackCooldown = ATTACK_COOLDOWN_TIME;
        skillCoolDown = skillCoolDownTime;
        isImmune = false;
        currentSkill = CurrentSkill.FireBall;
        transitionPanel = GameObject.FindWithTag("Transition");
        transitionPanel.SetActive(false);

        SceneManager.sceneLoaded += (var, var2) =>
        {
            transform.position = initPosition;
            initPosition = new Vector3(0, 0, 0);
        };
        footstepSound = Array.Find(FindObjectOfType<AudioManager>().sounds, s => s.name=="footstep").source;
    }

    private void Initialize()
    {
        playerStatus.Speed = 6;
        playerStatus.Attack = 10;
        playerStatus.Defense = 0;
        playerStatus.CurrentHP = 100;
        playerStatus.MaxHP = 100;
        playerStatus.CurrentMP = 100;
        playerStatus.MaxMP = 100;
        healthRegeneration = 0;
        manaRegeneration = 3;
    }

	// Update is called once per frame
	void Update ()
    {
        direction = joystick.Direction;
        Animation();
        Move();
        Attack();
        CoolDown();
        AttackDirection();
        if (isAttack)
        {
            speed = 0;
        } 
        else
        {
            speed = playerStatus.Speed;
        }

        if (immuneTimer < 0 && isImmune)
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

        if (isMove && !footstepSound.isPlaying) {
            footstepSound.Play();
        }



        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 castPoint;
            castPoint = Input.mousePosition;
            castPoint.z = 0.0f;
            Vector3 shootDirection = Camera.main.ScreenToWorldPoint(castPoint);
            touchDirection = shootDirection - transform.position;
            touchDirection.Normalize();
            if (isCoolDown)
            {
                if (currentSkill == CurrentSkill.FireBall && playerStatus.CurrentMP >= SKILL1_MANA_COST)
                {
                    FireBallAttack();
                    isAttack = true;
                    isCoolDown = false;
                    FireBallButton.SetCurrentCoolDown(0);
                }
                else if (currentSkill == CurrentSkill.Meteor && playerStatus.CurrentMP >= SKILL2_MANA_COST)
                {
                    MeteorAttack();
                    isAttack = true;
                    isCoolDown = false;
                    MeteorButton.SetCurrentCoolDown(0);
                }
                else if (currentSkill == CurrentSkill.Flame && playerStatus.CurrentMP >= SKILL3_MANA_COST)
                {
                    FlameAttack();
                    isAttack = true;
                    isCoolDown = false;
                    FlameButton.SetCurrentCoolDown(0);
                }
                else
                {
                    OutOfMana();
                }
                DirectionUpdate(new Vector2(castPoint.x - Screen.width / 2, castPoint.y - Screen.height / 2));
            }
		}
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
        if(playerStatus.CurrentHP<playerStatus.MaxHP){
            playerStatus.CurrentHP = playerStatus.CurrentHP + healthRegeneration;
        }
        if(playerStatus.CurrentMP< playerStatus.MaxMP){
            playerStatus.CurrentMP = playerStatus.CurrentMP + manaRegeneration;
        }


    }

    private void Move()
    {
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

    public int GetPlayerDamage()
    {
        return playerStatus.Attack;
    }

    private void DirectionUpdate(Vector2 direction)
    {
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
    }

    private void Decline(int[] properties) {
        playerStatus.MaxHP -= properties[0];
        if (playerStatus.CurrentHP > playerStatus.MaxHP) {
            playerStatus.CurrentHP = playerStatus.MaxHP;
        }
        playerStatus.Speed -= properties[1];
        playerStatus.Attack -= properties[2];
        playerStatus.Defense -= properties[3];
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
        if (collision.gameObject.tag == "Portal") {
            SceneManager.LoadScene("LevelTwo");
        }
    }

    private void Dash() {
        // this.speed = 30;
    }

    public void MeleeAttack() {

        if ( isAttack) {
            isAttack = true;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, 9);
            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<Enemy>().TakeDamage(playerStatus.Attack);
            }
            // Camera shake effect
            Vector3 deltaPosition = Vector3.zero;
            camera.transform.localPosition -= deltaPosition;
            deltaPosition = UnityEngine.Random.insideUnitCircle * 0.5f;
            camera.transform.position += deltaPosition;
        }
    }

    public void RestoreHealth(int health) {
        playerStatus.CurrentHP += health;
        if (playerStatus.CurrentHP > playerStatus.MaxHP) {
            playerStatus.CurrentHP = playerStatus.MaxHP;
        }
        PopupTextController.CreatePopupText(health.ToString(), transform, Color.green);
    }

    public void Getkey()
    {
        //Debug.Log("player371"+health);
        //PopupTextController.CreatePopupText("Door unlocked!", transform, Color.green);
        Broadcast("Next level door unlocked!", Color.green);
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
        if (!isImmune && !invincible) {
            immuneTimer = IMMUNE_TIME;
            damage = (int)(damage * (0.2+20/(float)(playerStatus.Defense+25)));
            playerStatus.CurrentHP -= damage;
            if (playerStatus.CurrentHP<=0) {
                FindObjectOfType<AudioManager>().Play("game_over");
                StartCoroutine(LoadScene("MainMenu"));
            }
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

    private void OutOfMana()
    {
        playerStatus.ShakeBar();
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

    // Skills
    public void ChangeSkill(string skill, float coolDownTime)
    {
        SetCurrentSkill(skill);
        SetCoolDownTime(coolDownTime);
        isCoolDown = true;
        skillCoolDown = skillCoolDownTime;
    }

    private void CoolDown()
    {
        if (!isCoolDown)
        {
            skillCoolDown -= Time.deltaTime;
            if (skillCoolDown < 0)
            {
                isCoolDown = true;
                skillCoolDown = skillCoolDownTime;
            }
        }
    }

    private void SetCurrentSkill(string skill)
    {
        if (skill.Equals("FireBall"))
        {
            currentSkill = CurrentSkill.FireBall;
        }
        else if (skill.Equals("Meteor"))
        {
            currentSkill = CurrentSkill.Meteor;
        }
        else if (skill.Equals("Flame"))
        {
            currentSkill = CurrentSkill.Flame;
        }
    }

    private void SetCoolDownTime(float cd)
    {
        skillCoolDownTime = cd;
    }

    private void FireBallAttack()
    {
        playerStatus.CurrentMP -= SKILL1_MANA_COST;
        FindObjectOfType<AudioManager>().Play("fire");
        var clone = Instantiate(fireBall, gameObject.transform.position + new Vector3(touchDirection.x, touchDirection.y, 0), gameObject.transform.rotation);

        float rot_z = Mathf.Atan2(touchDirection.y, touchDirection.x) * Mathf.Rad2Deg + 180f;
        clone.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        clone.GetComponent<Rigidbody2D>().velocity = touchDirection * 10f;

    }

    private void FlameAttack()
    {
        playerStatus.CurrentMP -= SKILL3_MANA_COST;
        FindObjectOfType<AudioManager>().Play("flame");
        var clone = Instantiate(flame, gameObject.transform.position, gameObject.transform.rotation);
        clone.transform.parent = gameObject.transform;
    }

    private void MeteorAttack() {
        playerStatus.CurrentMP -= SKILL2_MANA_COST;
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

        GameObject newMeteor = Instantiate(meteor) as GameObject;
        FindObjectOfType<Meteor>().Create(castPoint);
        newMeteor.transform.position = new Vector3(castPoint.x + 15, castPoint.y + 15, 0);
    }

    public void SetInvincible(bool b){
        invincible = b;
    }

    IEnumerator LoadScene(string name) {
        transitionPanel.SetActive(true);
        transition.SetTrigger("start");
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(name);
        transitionPanel.SetActive(false);
    }

    private void Broadcast(string content, Color color){
        GameObject canvas = GameObject.Find("Canvas");
        GameObject text = (GameObject)Resources.Load("Prefabs/Text");
        text = Instantiate(text);
        text.transform.SetParent(canvas.transform, false);
        Vector2 screenPosition = new Vector2(Screen.width / 2+100, Screen.height-100); 
        text.transform.position = screenPosition;
        text.GetComponent<UnityEngine.UI.Text>().text = content;
        text.GetComponent<UnityEngine.UI.Text>().color = color;
        Destroy(text, 5);
    }
}
