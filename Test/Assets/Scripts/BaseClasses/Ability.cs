using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class Ability : ScriptableObject
{
    [SerializeField]
    public string name;

    [SerializeField]
    public string description;

    [SerializeField]
    public AbilityDamage damegeTypes;

    [SerializeField]
    public List<AbilityStatus> status;

    [SerializeField]
    public GameObject prefab;

    [SerializeField]
    public float destroyTime;

    [SerializeField]
    public float castTime;
}


[System.Serializable]
public class AbilityDamage
{
    [SerializeField]
    public int max;

    [SerializeField]
    public int min;

    [SerializeField]
    public DameType damegeTypes;
}
[System.Serializable]
public class AbilityStatus
{
    [SerializeField]
    public int chance;

    [SerializeField]
    public BadStatus damegeTypes;
}
