using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public string tooltip;

    private bool tooltipMouseOn = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        CharacterCustomizationBehaviour.Instance.tooltipgameObject.GetComponentInChildren<Text>().text = tooltip;
        CharacterCustomizationBehaviour.Instance.tooltipgameObject.SetActive(true);
        tooltipMouseOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CharacterCustomizationBehaviour.Instance.tooltipgameObject.SetActive(false);
        tooltipMouseOn = false;
    }

    private void Update()
    {
        if (tooltipMouseOn)
        {
            CharacterCustomizationBehaviour.Instance.tooltipgameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
}
