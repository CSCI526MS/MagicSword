using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour, IPointerClickHandler {

    public float coolDown;
    public string skill;
    private float currentCoolDown = 0f;
    private bool isClick = false;

    void FixedUpdate()
    {
        if(isClick)
        {
            if (currentCoolDown >= coolDown)
            {
                GameObject.Find("Player").GetComponent<Player>().SetCurrentSkill(skill);
                currentCoolDown = 0f;
            }
            isClick = false;
        }
    }

    void Update()
    {
        if (currentCoolDown < coolDown)
        {
            currentCoolDown += Time.deltaTime;
            gameObject.GetComponent<Image>().fillAmount = currentCoolDown / coolDown;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        isClick = true;
    }


}

public class Skill
{
    public float coolDown;
    public Image skillIcon;
    [HideInInspector]
    public float currentCoolDown;
}
