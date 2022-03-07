using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemHelderBase : MonoBehaviour
{
    [SerializeField]
    public int slotQuantity;

    [HideInInspector]
    public GameObject mainMenu;

    public List<Slot> items = new List<Slot>();

    [HideInInspector]
    public MainMenu mainMenuBehaviour;

    [HideInInspector]
    public GameObject player;

    private void Start()
    {
        player = GameCenter.Instance.player;
    }

    public void Set(GameObject mainMenu)
    {
        this.mainMenu = GameCenter.Instance.mainMenu;
        this.mainMenuBehaviour = mainMenu.GetComponent<MainMenu>();
        RefreshItems();
    }

    public virtual void AddItem(Item item, bool draggable = true, bool showAction = true)
    {
        /*if (items.Where(x => x.item.name.Equals(item.name)).Count() == 0 || item.weapon != RPGCharacterAnims.Weapon.UNARMED)
        {
            if (items.Count() < slotQuantity)
            {
                items.Add(new Slot() { item = item, quantity = 1 , draggable  = draggable , showAction = showAction });
            }
        }
        else
        {
            items.Where(x => x.item.name.Equals(item.name)).FirstOrDefault().quantity++;
        }
        RefreshItems();*/
    }


    public virtual void RemoveItem(Item item)
    {
        if (items.Where(x => x.item.name.Equals(item.name)).Count() > 0)
        {
            Slot slot = items.Where(x => x.item.name.Equals(item.name)).FirstOrDefault();
            if (slot.quantity == 1)
            {
                items.Remove(items.Where(x => x.item.name.Equals(item.name)).FirstOrDefault());
            }
            else
            {
                items.Where(x => x.item.name.Equals(item.name)).FirstOrDefault().quantity--;
            }
            RefreshItems();
        }
    }

    public void RefreshItems()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var item in items)
        {
            var obj = GameObject.Instantiate(mainMenuBehaviour.itemPrefab);
            obj.GetComponent<PanelItemHandler>().Set(item, mainMenu);
            obj.transform.SetParent(this.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
            obj.SetActive(true); //Activate the GameObject
            item.itemGameObject = obj;
        }

        for (int i = 0; i < slotQuantity - items.Count(); i++)
        {
            GameObject NewObj = new GameObject(); //Create the GameObject
            Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
            NewImage.sprite = mainMenuBehaviour.emptySlotImage; //Set the Sprite of the Image Component on the new GameObject
            NewObj.GetComponent<RectTransform>().SetParent(this.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
            NewObj.SetActive(true); //Activate the GameObject
        }
    }
}
