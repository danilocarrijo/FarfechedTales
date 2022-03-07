using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControler : MonoBehaviour
{
    [HideInInspector]
    private ItemGrid selectedItemGrid;

    public ItemGrid SelectedItemGrid { 
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            inventoryHighlight.SetParent(SelectedItemGrid);
        }
    }

    InventoryItem selectedItem;
    RectTransform rectTransform;
    InventoryItem overlapitem;

    [SerializeField]
    List<ItemData> items;

    [SerializeField] 
    GameObject itemPrefab;

    [SerializeField]
    Transform canvasTransform;

    InventoryHighlight inventoryHighlight;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            InsertRandomItem();
        }

        if (selectedItemGrid == null)
        {
            inventoryHighlight.Show(false);
            return;
        }

        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPressed();
        }
    }

    private void InsertRandomItem()
    {
        CreateRandomItem();
        InventoryItem itemInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemInsert);
    }

    private void InsertItem(InventoryItem itemInsert)
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemInsert);

        if (!posOnGrid.HasValue)
            return;

        selectedItemGrid.PlaceItem(itemInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    InventoryItem itemToHighlight;
    Vector2Int oldPosition;

    

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTitleGridPosition();

        if (oldPosition == positionOnGrid)
            return;

        if (selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (itemToHighlight != null) 
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
        }
        else
        {
            inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x,
                positionOnGrid.y,
                selectedItem.itemData.width, 
                selectedItem.itemData.height));
            inventoryHighlight.SetSize(selectedItem);
            inventoryHighlight.SetPosition(selectedItemGrid, selectedItem,positionOnGrid.x,positionOnGrid.y);
        }
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        int selectItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectItemID]);
    }

    private void LeftMouseButtonPressed()
    {
        Vector2Int titleGridPosition = GetTitleGridPosition();

        if (selectedItem == null)
        {
            PickUpItem(titleGridPosition);
        }
        else
        {
            PlaceItem(titleGridPosition);
        }
    }

    private Vector2Int GetTitleGridPosition()
    {
        Vector2 position = Input.mousePosition;

        /*if(selectedItem != null)
        {
            position.x -= (selectedItem.itemData.width - 1) * ItemGrid.titleSizeWidth / 2;
            position.y += (selectedItem.itemData.height - 1) * ItemGrid.titleSizeHeigth / 2;
        }
        */
        Vector2Int titleGridPosition = selectedItemGrid.GeGridtPosition(position);
        return titleGridPosition;
    }

    private void PlaceItem(Vector2Int titleGridPosition)
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, titleGridPosition.x, titleGridPosition.y,ref overlapitem);
        if (complete)
        {
            selectedItem = null;
            if(overlapitem != null)
            {
                selectedItem = overlapitem;
                overlapitem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }

    private void PickUpItem(Vector2Int titleGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(titleGridPosition.x, titleGridPosition.y);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
