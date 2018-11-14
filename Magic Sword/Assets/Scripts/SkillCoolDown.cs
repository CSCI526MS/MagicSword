using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public float coolDownTime;
    public string skill;
    public Sprite normalImage;
    public Sprite selectedImage;
    private float currentCoolDown = 0f;
    private bool isClick = false;
    private bool touch;

    void Start()
    {
        currentCoolDown = coolDownTime;
        touch = false;
    }

    void FixedUpdate()
    {
        if(isClick)
        {
            if (currentCoolDown >= coolDownTime)
            {
                GameObject.Find("Player").GetComponent<Player>().ChangeSkill(skill, coolDownTime);
                GameObject.Find("FireBallCooldown").GetComponent<SkillCoolDown>().deactive();
                GameObject.Find("FlameCooldown").GetComponent<SkillCoolDown>().deactive();
                GameObject.Find("MeteorCooldown").GetComponent<SkillCoolDown>().deactive();
                active();
                //currentCoolDown = 0f;
            }
            isClick = false;
        }
    }

    void Update()
    {
        if (currentCoolDown < coolDownTime)
        {
            currentCoolDown += Time.deltaTime;
            gameObject.GetComponent<Image>().fillAmount = currentCoolDown / coolDownTime;
        }
    }

    public void active()
    {
        gameObject.GetComponent<Image>().sprite = selectedImage;
    }

    public void deactive()
    {
        gameObject.GetComponent<Image>().sprite = normalImage;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
        touch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touch = false;
    }

    public void SetCurrentCoolDown(float ccd)
    {
        currentCoolDown = ccd;
    }

}

public class Skill
{
    public float coolDown;
    public Image skillIcon;
    [HideInInspector]
    public float currentCoolDown;
}
