using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour, IPointerClickHandler {

    public float coolDownTime;
    public string skill;
    private float currentCoolDown = 0f;
    private bool isClick = false;

    void Start()
    {
        currentCoolDown = coolDownTime;
    }

    void FixedUpdate()
    {
        if(isClick)
        {
            if (currentCoolDown >= coolDownTime)
            {
                GameObject.Find("Player").GetComponent<Player>().ChangeSkill(skill, coolDownTime);
                currentCoolDown = 0f;
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


    public void OnPointerClick(PointerEventData eventData)
    {
        //GameObject.Find("Player").GetComponent<Player>().SetCoolDownTime(coolDown);
        isClick = true;
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
