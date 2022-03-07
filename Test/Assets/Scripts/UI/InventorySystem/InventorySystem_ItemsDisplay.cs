using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PlayerBehavor;

public class InventorySystem_ItemsDisplay : MonoBehaviour
{
    public GameObject slotPrefab;

    private GameObject player;

    private static List<GameObject> slots = new List<GameObject>();

    InventoryChangeHandler inventoryChangeHandler;



    // Start is called before the first frame update
    void Start()
    {
        player = GameCenter.Instance.player;
        player.GetComponent<PlayerBehavor>().OnInventoryChange += new PlayerBehavor.InventoryChangeHandler(ArrangeItems);
        for (int i = 0; i < player.GetComponent<PlayerBehavor>().maxSlots; i++)
        {
            var obj = GameObject.Instantiate(slotPrefab);
            obj.GetComponent<Slotbehaviour>().position = i;
            obj.GetComponent<Slotbehaviour>().slotType = SlotType.ITEM;
            obj.transform.SetParent(this.transform);
            obj.SetActive(true);
            slots.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static GameObject NextEmpytSlot()
    {
        return slots.Where(x => x.GetComponent<Slotbehaviour>().item == null).FirstOrDefault();
    }

    private IEnumerator ArrangeItemsCorroutine()
    {
        foreach (var item in slots)
        {
            item.GetComponent<Slotbehaviour>().item = null;
        }
        foreach (var item in player.GetComponent<PlayerBehavor>().GetItems())
        {
            var slot = slots.Where(x => x.GetComponent<Slotbehaviour>().position == item.slot).FirstOrDefault();
            if (slot != null)
            {
                slot.GetComponent<Slotbehaviour>().item = item;
            }
        }
        yield return null;
    }

    private void ArrangeItems()
    {
        StartCoroutine(ArrangeItemsCorroutine());
    }
}
