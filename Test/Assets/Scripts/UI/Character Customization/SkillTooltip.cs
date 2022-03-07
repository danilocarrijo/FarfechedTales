using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector]
    public Skill skill;
    private GameObject traitTooltip;
    private bool tooltipMouseOn = false;
    private GameObject character;

    private void Start()
    {
        traitTooltip = CharacterCustomizationBehaviour.Instance.skillooltip;
        character = CharacterCustomizationBehaviour.Instance.character;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        traitTooltip.transform.Find("Title").GetComponent<Text>().text = skill.skill.ToString();
        traitTooltip.transform.Find("Description").GetComponent<Text>().text = skill.description;
        tooltipMouseOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipMouseOn = false;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (character.GetComponent<PlayerBehavor>().infos.skills.Where(x => x.skill == skill.skill).Count() == 0
            && character.GetComponent<PlayerBehavor>().infos.skills.Count() < CharacterCustomizationBehaviour.Instance.maxSkills)
        {
            character.GetComponent<PlayerBehavor>().infos.skills.Add(skill);
            this.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            character.GetComponent<PlayerBehavor>().infos.skills.Remove(skill);
            this.GetComponent<Image>().color = Color.white;
        }
    }

    private void Update()
    {
        if (tooltipMouseOn)
        {
            traitTooltip.transform.Find("Title").GetComponent<Text>().text = skill.skill.ToString();
            traitTooltip.transform.Find("Description").GetComponent<Text>().text = skill.description;
        }
    }
}
