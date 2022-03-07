using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AmbitionsTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector]
    public Ambition ambition;
    private GameObject ambitionTooltip;
    private bool tooltipMouseOn = false;
    private GameObject character;
    private GameObject ambitionsTooltipActivityList;
    private GameObject ambitionsToolTaskPrefab;

    private void Start()
    {
        ambitionTooltip = CharacterCustomizationBehaviour.Instance.ambitionsTooltip;
        character = CharacterCustomizationBehaviour.Instance.character;
        ambitionsTooltipActivityList = CharacterCustomizationBehaviour.Instance.ambitionsTooltipActivityList;
        ambitionsToolTaskPrefab = CharacterCustomizationBehaviour.Instance.ambitionsToolTaskPrefab;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ambitionTooltip.transform.Find("Title").GetComponent<Text>().text = ambition.name;
        foreach (Transform child in ambitionsTooltipActivityList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        tooltipMouseOn = true;
        StartCoroutine(SetTask());
    }

    private IEnumerator SetTask()
    {

        foreach (var item in ambition.tasks)
        {
            yield return new WaitForSeconds(0.01f);
            var newGO = GameObject.Instantiate(ambitionsToolTaskPrefab);
            newGO.GetComponent<Text>().text = item.Value;
            newGO.transform.SetParent(ambitionsTooltipActivityList.transform);

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipMouseOn = false;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (character.GetComponent<PlayerBehavor>().infos.ambitions.Where(x => x.name == ambition.name).Count() == 0
            && character.GetComponent<PlayerBehavor>().infos.ambitions.Count() < CharacterCustomizationBehaviour.Instance.maxAmbitions)
        {
            character.GetComponent<PlayerBehavor>().infos.ambitions.Add(ambition);
            this.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            character.GetComponent<PlayerBehavor>().infos.ambitions.Remove(ambition);
            this.GetComponent<Image>().color = Color.white;
        }
    }

    private void Update()
    {
        if (tooltipMouseOn)
        {
            ambitionTooltip.transform.Find("Title").GetComponent<Text>().text = ambition.name;
        }
    }
}
