using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject OptionMenu;
    [SerializeField]
    public GameObject DescriptionMenu;
    [SerializeField]
    public GameObject draggableItemPrefab;
    [SerializeField]
    public GameObject craftPanel;
    [SerializeField]
    public GameObject inventoryPanel;
    [SerializeField]
    public GameObject weaponEquipPanel;
    [SerializeField]
    public Sprite emptySlotImage;
    [SerializeField]
    public GameObject itemPrefab;


    public GameObject draggingItem;
    public MouseArea? currMouseArea;
    public MouseArea? lastMouseArea;
    private CraftBehaviour craftBehaviour;
    private InventoryBehaviour inventoryBehaviour;
    private Equip_Behaviour weaponEquipBehaviour;

    private void Start()
    {
    }

    // Start is called before the first frame update
    public void Set() 
    {
        craftBehaviour = craftPanel.GetComponent<CraftBehaviour>();
        inventoryBehaviour = inventoryPanel.GetComponent<InventoryBehaviour>();
        weaponEquipBehaviour = weaponEquipPanel.GetComponent<Equip_Behaviour>();
        craftBehaviour.Set(this.gameObject);
        inventoryBehaviour.Set(this.gameObject);
        weaponEquipBehaviour.Set(this.gameObject);
        weaponEquipBehaviour.player = GameCenter.Instance.player;
        //craftBehaviour.Set(this, itemPrefab, emptySlotImage, this);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetDragInit(Slot item)
    {
        var obj = GameObject.Instantiate(draggableItemPrefab);
        obj.GetComponent<DraggableItemBehavor>().Set(item);
        obj.transform.SetParent(this.transform);
        obj.SetActive(true); //Activate the GameObject
        draggingItem = obj;
    }

    public void AddItemInventory(Item item)
    {
        inventoryBehaviour.AddItem(item);
    }

    public void SetDragEnd()
    {
        if (currMouseArea == MouseArea.CRAFT && lastMouseArea == MouseArea.INVENTORY)
        {
            craftBehaviour.AddItem(draggingItem.GetComponent<DraggableItemBehavor>().item.item);
            inventoryBehaviour.RemoveItem(draggingItem.GetComponent<DraggableItemBehavor>().item.item);
        }
        else if (currMouseArea == MouseArea.INVENTORY && lastMouseArea == MouseArea.CRAFT)
        {
            inventoryBehaviour.AddItem(draggingItem.GetComponent<DraggableItemBehavor>().item.item);
            craftBehaviour.RemoveItem(draggingItem.GetComponent<DraggableItemBehavor>().item.item);
        }
        if (currMouseArea == MouseArea.WEAPON_EQUIP && lastMouseArea == MouseArea.INVENTORY)
        {
            try
            {

                weaponEquipBehaviour.AddItem(draggingItem.GetComponent<DraggableItemBehavor>().item.item);
                inventoryBehaviour.RemoveItem(draggingItem.GetComponent<DraggableItemBehavor>().item.item);
            }
            catch (System.Exception){}
        }
        if (currMouseArea == MouseArea.INVENTORY && lastMouseArea == MouseArea.WEAPON_EQUIP)
        {
            weaponEquipBehaviour.RemoveItem(draggingItem.GetComponent<DraggableItemBehavor>().item.item);
            inventoryBehaviour.AddItem(draggingItem.GetComponent<DraggableItemBehavor>().item.item);
        }
        GameObject.Destroy(draggingItem);
        draggingItem = null;
    }

    public void SetMouseArea(MouseArea? mouseArea)
    {
        if (mouseArea == null)
            lastMouseArea = currMouseArea;
        currMouseArea = mouseArea;
    }
}

public enum MouseArea
{
    CRAFT,
    INVENTORY,
    WEAPON_EQUIP
}
