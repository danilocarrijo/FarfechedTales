using RPGCharacterAnims;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static BaseClass;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public List<string> types;
    List<string> damageTypes;
    List<string> rarity;
    List<string> weapons;
    List<string> weaponSubTypes;
    List<string> itemModifiers;
    List<string> itemModifierTypes;
    List<string> stats;

    Item myTarget;

    void OnEnable()
    {
        myTarget = (Item)target;
        damageTypes  = Enum.GetNames(typeof(DameType)).ToList();
        rarity = Enum.GetNames(typeof(ItemRarity)).ToList();
        weapons = Enum.GetNames(typeof(WeaponTypeEnum)).ToList();
        weaponSubTypes = Enum.GetNames(typeof(WeaponSubType)).ToList();
        itemModifierTypes = Enum.GetNames(typeof(ItemModifierType)).ToList();
        stats = Enum.GetNames(typeof(Stats)).ToList();
        types = Enum.GetNames(typeof(ItemType)).ToList();
    }
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Spawn Prop");

        myTarget.name = EditorGUILayout.TextField("Name", myTarget.name);

        myTarget.description = EditorGUILayout.TextField("Description", myTarget.description);

        myTarget.icon = (Sprite)EditorGUILayout.ObjectField("Icon", myTarget.icon, typeof(Sprite),true);

        myTarget.itemPrefab = (GameObject)EditorGUILayout.ObjectField("Item Prefab", myTarget.itemPrefab, typeof(GameObject), true);

        myTarget.rarity = (ItemRarity)EditorGUILayout.Popup((int)myTarget.rarity, rarity.ToArray());


        EditorGUILayout.Space();

        myTarget.type = (ItemType)EditorGUILayout.Popup((int)myTarget.type, types.ToArray());

        switch (myTarget.type)
        {
            case ItemType.WEAPON:
                RenderWaepon();
                myTarget.itemModifiers = new List<ItemModifier>();
                break;
            default:
                RenderItem();
                myTarget.weaponModifiers = new List<WeaponMidifier>();
                break;
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Create items inside of ItemDatabase.cs", MessageType.Info);
        EditorGUILayout.Space();
    }

    private void RenderItem()
    {
        ItemModifier removeIndex = null;

        foreach (var item in myTarget.itemModifiers)
        {
            var index = 0;
            GUILayout.BeginHorizontal("BOX");
            GUILayout.BeginVertical("BOX");
            EditorGUILayout.Space();
            item.modifierImpact = EditorGUILayout.IntField("Amount To Modify", item.modifierImpact);
            item.modifierType = (ItemModifierType)EditorGUILayout.Popup("Modifier", (int)item.modifierType, itemModifierTypes.ToArray());
            if(item.modifierType == ItemModifierType.ADD_FOR_TIME)
            {
                item.time = EditorGUILayout.IntField("Time", item.time);
            }
            else
            {
                item.time = 0;
            }
            item.statsToModify = (Stats)EditorGUILayout.Popup("Attribute To Modify", (int)item.statsToModify, stats.ToArray());
            EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();
            if (GUILayout.Button("Remove Modifier"))
            {
                removeIndex = item;
            }
            index++;
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }


        if (GUILayout.Button("Add item Modifier"))
        {
            myTarget.itemModifiers.Add(new ItemModifier());
        }

        if (removeIndex != null)
        {
            myTarget.itemModifiers.Remove(removeIndex);
        }
    }

    private void RenderWaepon()
    {
        WeaponMidifier removeIndex = null;
        GUILayout.BeginHorizontal("BOX");
        GUILayout.BeginVertical("BOX");
        EditorGUILayout.Space();

        myTarget.weapon = (WeaponTypeEnum)EditorGUILayout.Popup("Weapon Type: ",(int)myTarget.weapon, weapons.ToArray());
        myTarget.WeaponType = (WeaponSubType)EditorGUILayout.Popup("Weapon Sub Type: ",(int)myTarget.WeaponType, weaponSubTypes.ToArray());
        myTarget.attackDistance = EditorGUILayout.IntField("Attack Distance: ", myTarget.attackDistance);

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        foreach (var item in myTarget.weaponModifiers)
        {
            var index = 0;
            GUILayout.BeginHorizontal("BOX");
            GUILayout.BeginVertical("BOX");
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Description: " + item.dameType);
            item.minType = EditorGUILayout.IntField("Min Damage",item.minType);
            item.maxType = EditorGUILayout.IntField("Max Damage", item.maxType);
            item.dameType = (DameType)EditorGUILayout.Popup("Damage Type", (int)item.dameType, damageTypes.ToArray());
            EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();
            if (GUILayout.Button("Remove Modifier"))
            {
                removeIndex = item;
            }
            index++;
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }


        if (GUILayout.Button("Add weapon Modifier"))
        {
            myTarget.weaponModifiers.Add(new WeaponMidifier());
        }

        if (removeIndex != null)
        {
            myTarget.weaponModifiers.Remove(removeIndex);
        }
    }

}
