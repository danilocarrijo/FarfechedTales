using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TraitTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector]
    public Trait trait;
    private GameObject traitTooltip;
    private bool tooltipMouseOn = false;
    private GameObject character;

    private void Start()
    {
        traitTooltip = CharacterCustomizationBehaviour.Instance.traitTooltip;
        character = CharacterCustomizationBehaviour.Instance.character;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        traitTooltip.transform.Find("Title").GetComponent<Text>().text = trait.trait.ToString();
        traitTooltip.transform.Find("Description").GetComponent<Text>().text = trait.description;
        traitTooltip.transform.Find("Benefits").GetComponent<Text>().text = trait.benefit;
        traitTooltip.transform.Find("Drawback").GetComponent<Text>().text = trait.drawback;
        tooltipMouseOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipMouseOn = false;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (character.GetComponent<PlayerBehavor>().infos.traits.Where(x => x.trait == trait.trait).Count() == 0
            && character.GetComponent<PlayerBehavor>().infos.traits.Count() < CharacterCustomizationBehaviour.Instance.maxTraits)
        {
            character.GetComponent<PlayerBehavor>().infos.traits.Add(trait);
            this.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            character.GetComponent<PlayerBehavor>().infos.traits.Remove(trait);
            this.GetComponent<Image>().color = Color.white;
        }
    }

    private void Update()
    {
        if (tooltipMouseOn)
        {
            traitTooltip.transform.Find("Title").GetComponent<Text>().text = trait.trait.ToString();
            traitTooltip.transform.Find("Description").GetComponent<Text>().text = trait.description;
            traitTooltip.transform.Find("Benefits").GetComponent<Text>().text = trait.benefit;
            traitTooltip.transform.Find("Drawback").GetComponent<Text>().text = trait.drawback;
        }
    }
}
