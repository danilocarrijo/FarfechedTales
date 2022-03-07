using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trait
{
    [SerializeField]
    public Trait_Enum trait;
    [SerializeField]
    public string description;
    [SerializeField]
    public Sprite icon; 
    [SerializeField]
    public string benefit;
    [SerializeField]
    public string drawback;
}
