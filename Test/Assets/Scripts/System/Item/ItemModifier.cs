using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseClass;

[System.Serializable]
public class ItemModifier 
{
    [SerializeField]
    public ItemModifierType modifierType;
    [SerializeField]
    public int modifierImpact;
    [SerializeField]
    public int time;
    [SerializeField]
    public Stats statsToModify;

}
