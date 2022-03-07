using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableItemBehavor : MonoBehaviour
{
    public Slot item;

    public void Set(Slot item)
    {
        this.item = item;
        this.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = item.item.icon;
    }

    private void Update()
    {
        this.transform.position = Input.mousePosition;
    }
}
