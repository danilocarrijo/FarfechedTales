using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnstashEvent
{
    [SerializeField]
    public WeaponTypeEnum Weapon;
    [SerializeField]
    public float anim;
    [SerializeField]
    public float animLenght;
}
