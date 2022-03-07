using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GAMEAREA gameArea;
    public Vector3? currPos;

    private DragHandler canvas;
    public void OnPointerEnter(PointerEventData eventData)
    {
        canvas.currGameArea = gameArea;
    }


    public void HideUnHide()
    {
        if(currPos == null)
        {
            currPos = this.GetComponent<RectTransform>().localPosition;
            this.GetComponent<RectTransform>().localPosition = new Vector3(-2000, 0);
        }
        else
        {
            this.GetComponent<RectTransform>().localPosition = currPos.Value;
            currPos = null;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvas.currGameArea = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<DragHandler>();
        HideUnHide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
