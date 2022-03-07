using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableButton : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    [SerializeField]
    public List<GameObject> targetToDesable;
    public void Set()
    {
        if(target == null)
            this.gameObject.SetActive(!this.gameObject.active);
        else
        {
            target.SetActive(!target.active);
        }
        if(targetToDesable != null && targetToDesable.Count > 0)
        {
            foreach (var item in targetToDesable)
            {
                item.SetActive(false);
            }
        }
    }
}
