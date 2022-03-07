using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerBehavor : MonoBehaviour
{
    public BaseClass infos;

    private Moviment moviment;


    private InventoryBehaviour inventory;

    [SerializeField]
    public GameObject mainMenu;

    [HideInInspector]
    public GameObject characterObject;

    private GameCenter gameCenter;

    private List<Item> items = new List<Item>();

    public int maxSlots = 10;


    public delegate void InventoryChangeHandler();

    public event InventoryChangeHandler OnInventoryChange;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        gameCenter = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameCenter>();
        moviment = this.transform.GetComponent<Moviment>();
        characterObject = this.transform.GetChild(0).gameObject;
    }

    public void EquilWeapon(Item item)
    {
        characterObject.GetComponent<Moviment>().EquipWeapon(item);
    }

    public void UnequilWeapon()
    {
        characterObject.GetComponent<Moviment>().UnequilWeapon();
    }

    public void AddItem(Item item)
    {
        try
        {
            int slotPos = LookForEmptySlot();
            item.slot = slotPos;
            items.Add(item);
            InventoryChange();
        }
        catch (System.Exception)
        {
            throw new System.Exception();
        }
    }

    private int LookForEmptySlot()
    {
        var slot = InventorySystem_ItemsDisplay.NextEmpytSlot();
        if(slot != null)
        {
            return slot.GetComponent<Slotbehaviour>().position;
        }
        throw new System.Exception();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        InventoryChange();
    }

    protected void InventoryChange()
    {
        OnInventoryChange?.Invoke();
    }

    public List<Item> GetItems()
    {
        return this.items;
    }

    public void SetItemSlot()
    {
        items[1].slot = 6;
        InventoryChange();
    }

    public void SwapSlot(int origin, int destiny)
    {
        var originObj = items.FirstOrDefault(x => x.slot == origin);
        var destinyObj = items.FirstOrDefault(x => x.slot == destiny);
        originObj.slot = destiny;
        if (destinyObj != null)
        {
            destinyObj.slot = origin;
        }
        InventoryChange();
    }
    void Update()
    {
    }
}
