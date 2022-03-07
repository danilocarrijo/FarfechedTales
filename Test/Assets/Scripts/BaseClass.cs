using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseClass
{

    public enum Stats
    {
        HUNGRY,
        SLEEP,
        STRENGTH,
        DEXTERITY,
        INTELIGENCE,
        ENERGY,
        HEALTH,
        RESISTENCE,
        FOCUS,
        AGILITY,
        DEFENSE,
        ENDURANCE,
        BASE_DEMAGE,
        CHARISMA,
        WILL,
        HABILITY
    }

    public enum genders
    {
        MALE,
        FEMALE
    }

    [Header("Infos")]
    public string name;
    public float cur_Level;
    public float cur_XP;
    public float cur_Heath;
    public float cur_Mana;
    public float max_Heath;
    public float max_Mana;

    [Header("Gender")]
    public genders gender;

    [Header("Status")]
    [SerializeField]
    public Dictionary<Stats, int> baseStas;
    public int strenhtg;
    public int endurance;
    public int dexterity;
    public int will;
    public int agility;
    public int wisdom;
    public int charisma;
    public int inteligence;
    public int baseAttack;
    public int evade;
    public int armorClass;


    public List<Trait> traits;
    public List<Skill> skills;
    public List<Ambition> ambitions;

    [Header("Skills")]
    public int statPoints;
    public int skillPoints;


    [Header("Resistences")]
    public List<Resistence> resistencePercentages = new List<Resistence>();



    [SerializeField]
    public ItemRarity enemyRarity;

}
