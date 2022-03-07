using RPGCharacterAnims;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class Item : ScriptableObject, ICloneable
{
    [SerializeField]
    public string name;

    [SerializeField]
    public string description;

    [SerializeField]
    public List<ItemModifier> itemModifiers;

    [SerializeField]
    public List<WeaponMidifier> weaponModifiers;

    [SerializeField]
    public int attackDistance;

    [SerializeField]
    public Sprite icon;

    [SerializeField]
    public ItemRarity rarity; 

    [SerializeField]
    public WeaponTypeEnum weapon;

    [SerializeField]
    public WeaponSubType WeaponType;

    public ItemType type;

    [HideInInspector]
    public int slot;

    

    [SerializeField]
    public GameObject itemPrefab;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
