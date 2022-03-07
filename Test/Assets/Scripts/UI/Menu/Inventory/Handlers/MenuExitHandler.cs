using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuExitHandler : MonoBehaviour, IPointerExitHandler// required interface when using the OnPointerDown method.
{

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
        this.gameObject.SetActive(false);
    }
}
