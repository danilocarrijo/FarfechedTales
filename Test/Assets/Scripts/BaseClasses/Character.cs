using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class Character : ScriptableObject
{
    [SerializeField]
    GameObject face;

    [SerializeField]
    GameObject hair;
}
