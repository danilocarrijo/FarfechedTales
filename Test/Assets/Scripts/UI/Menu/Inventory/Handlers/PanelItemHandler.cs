using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelItemHandler : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler,IEndDragHandler
{
    private GameObject DescriptionMenu;
    //private Slot item;
    private MainMenu mainMenu;
    private bool draggable;
    private bool showAction;
    private GameObject descriptionMenu;

    [SerializeField]
    public Item item;

    public void Set(Slot item, GameObject mainMenu,bool draggable = true, bool showAction = true)
    {
        descriptionMenu = GameObject.FindGameObjectWithTag("DescriptionMenu");
        /*this.item = item;
        this.draggable = draggable;
        this.showAction = showAction;
        this.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = item.item.icon;
        this.transform.Find("Quantity").gameObject.GetComponent<Text>().text = item.quantity.ToString();
        this.mainMenu = mainMenu.GetComponent<MainMenu>();
        switch (item.item.rarity)
        {
            case ItemRarity.COMMON:
                this.transform.GetComponent<Image>().sprite = this.mainMenu.commonBackground;
                break;
            case ItemRarity.UNCOMMON:
                this.transform.GetComponent<Image>().sprite = this.mainMenu.uncommonBackground;
                break;
            case ItemRarity.RARE:
                this.transform.GetComponent<Image>().sprite = this.mainMenu.rareBackground;
                break;
            case ItemRarity.LEGANDARY:
                this.transform.GetComponent<Image>().sprite = this.mainMenu.legendaryBackground;
                break;
            case ItemRarity.UNIQUE:
                this.transform.GetComponent<Image>().sprite = this.mainMenu.uniqueBackground;
                break;
        }*/
    }

    private void Update()
    {
       /* if (DescriptionMenu != null)
        {
            DescriptionMenu.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 250);
        }*/
    }

    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        /*if (((int)eventData.button) == 1 && showAction)
        {
            Debug.Log("Pressed right click.");
            var OptionMenu = mainMenu.GetComponent<MainMenu>()?.OptionMenu;
            if(OptionMenu != null)
            {
                OptionMenu.SetActive(true);
                OptionMenu.transform.position = Input.mousePosition;
            }
        }*/
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       /* DescriptionMenu = GameObject.Find("MainMenu")?.GetComponent<MainMenu>().DescriptionMenu;
        if (DescriptionMenu != null)
        {
            DescriptionMenu.SetActive(true);
        }*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //DescriptionMenu.SetActive(false);
        //DescriptionMenu = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(item.name);
        /*if (draggable)
        {
            mainMenu.SetDragInit(this.item);
        }*/
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameCenter.Instance.player.GetComponent<PlayerBehavor>().SetItemSlot();
        Debug.Log(item.name);
        /*if (draggable)
        {
            mainMenu.SetDragEnd();
        }*/
    }
}