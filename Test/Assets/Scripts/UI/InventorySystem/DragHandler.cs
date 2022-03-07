using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour
{
    public GameObject dragPanel;

    private Slotbehaviour _currSlot;
    private bool isDragging;

    [HideInInspector]
    public Slotbehaviour currSlot { get; set; }

    [HideInInspector]
    public GAMEAREA? currGameArea { get; set; }

    [HideInInspector]
    public Slotbehaviour currDragSlot { 
        set {
            _currSlot = value;
            if(_currSlot != null)
            {
                dragPanel.SetActive(true);
                dragPanel.GetComponent<Image>().sprite = _currSlot.item.icon;
                isDragging = true;
            }
            else
            {
                dragPanel.SetActive(false);
                isDragging = false;
            }
        }
        get
        {
            return _currSlot;
        }
    }


    [HideInInspector]
    public int originDragSlot;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            dragPanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
}
