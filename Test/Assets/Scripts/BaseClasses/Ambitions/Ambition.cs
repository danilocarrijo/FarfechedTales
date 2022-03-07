using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ambition
{

    public Dictionary<int,string> tasks = new Dictionary<int, string>();
    public Dictionary<int, string> completedTasks = new Dictionary<int, string>();
    public AmbitionType type;
    public Sprite icon;
    public string name;
   
    public virtual void Set(GameObject target)
    {

    }
}

[System.Serializable]
public class AmbitionIcon
{
    public string name;
    public Sprite icon;
}
