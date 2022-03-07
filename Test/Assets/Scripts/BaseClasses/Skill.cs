using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    [SerializeField]
    public Skill_Enum skill;
    [SerializeField]
    public Skill_Enum parentSkill;
    [SerializeField]
    public string description;
    [SerializeField]
    public Sprite icon;
}
