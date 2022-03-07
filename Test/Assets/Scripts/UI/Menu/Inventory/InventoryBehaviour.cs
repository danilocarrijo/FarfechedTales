using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryBehaviour : ItemHelderBase, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter InventoryBehaviour");
        mainMenuBehaviour.SetMouseArea(MouseArea.INVENTORY);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit InventoryBehaviour");
        mainMenuBehaviour.SetMouseArea(null);
    }
}

public class Slot
{
    public Item item;
    public GameObject itemGameObject;
    public int quantity;
    public bool draggable = true;
    public bool showAction = true;
}