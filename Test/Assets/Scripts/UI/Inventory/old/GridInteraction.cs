using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteraction : MonoBehaviour, IPointerExitHandler,IPointerEnterHandler
{
    InventoryControler inventoryControler;
    public ItemGrid itemGrid;

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryControler.SelectedItemGrid = itemGrid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryControler.SelectedItemGrid = null;
    }

    private void Awake()
    {
        inventoryControler = FindObjectOfType(typeof(InventoryControler)) as InventoryControler;
        itemGrid = GetComponent<ItemGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
