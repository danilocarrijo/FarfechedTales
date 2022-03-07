using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Handler : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> itemsToActivateOnClick;

    // Start is called before the first frame update
    public void SetMenuSreen(int area)
    {
        foreach (var item in itemsToActivateOnClick)
        {
            item.gameObject.SetActive(!item.gameObject.activeInHierarchy);
        }
    }  
}


