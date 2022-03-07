using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Equip_Behaviour : ItemHelderBase, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public MouseArea area;

    public void OnPointerEnter(PointerEventData eventData)
    {
        mainMenuBehaviour.SetMouseArea(area);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mainMenuBehaviour.SetMouseArea(null);
    }

    public override void AddItem(Item item, bool draggable = true, bool showAction = true)
    {
        if (items.Count() > 0)
            throw new Exception();
        if(area == MouseArea.WEAPON_EQUIP)
        {
            player.GetComponent<Moviment>().EquipWeapon(item);
        }
        base.AddItem(item);
    }

    public override void RemoveItem(Item item)
    {
        if (area == MouseArea.WEAPON_EQUIP)
        {
            //player.GetComponent<Moviment>().UnEquipWeapon();
        }
        base.RemoveItem(item);
    }
}
