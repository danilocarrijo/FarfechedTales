using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float titleSizeWidth = 32;
    public const float titleSizeHeigth = 32;

    RectTransform rectTransform;

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int titleGridPosition = new Vector2Int();

    InventoryItem[,] inventoryItemSlot;

    [SerializeField] int gridSizeWidth = 20;
    [SerializeField] int gridSizeHeight = 20;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(10, 10);
    }

    internal InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null) { return null; }

        ClearGridReference(toReturn);

        return toReturn;

    }

    private void ClearGridReference(InventoryItem item)
    {
        for (int i = 0; i < item.itemData.width; i++)
        {
            for (int iy = 0; iy < item.itemData.height; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + i, item.onGridPositionY + iy] = null;
            }
        }
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * titleSizeWidth, height * titleSizeHeigth);
        rectTransform.sizeDelta = size;
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }

    public Vector2Int GeGridtPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        titleGridPosition.x = (int)(positionOnTheGrid.x / titleSizeWidth);
        titleGridPosition.y = (int)(positionOnTheGrid.y / titleSizeHeigth);

        print(titleGridPosition);

        return titleGridPosition;

    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemInsert)
    {
        int height = gridSizeHeight - itemInsert.itemData.height;
        int width = gridSizeWidth - itemInsert.itemData.width;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if(CheckAvailablaSpace(x,y, itemInsert.itemData.width, itemInsert.itemData.height))
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return null;
    }

    public bool PlaceItem(InventoryItem inventoryItem,int posX, int posY,ref InventoryItem overlapitem)
    {
        if (!BoundryCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.height))
        {
            return false; ;
        }

        if (!OverlpaCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.height, ref overlapitem))
        {
            overlapitem = null;
            return false;
        }

        if (overlapitem != null)
        {
            ClearGridReference(overlapitem);
        }

        PlaceItem(inventoryItem, posX, posY);

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);


        for (int i = 0; i < inventoryItem.itemData.width; i++)
        {
            for (int y = 0; y < inventoryItem.itemData.height; y++)
            {

                inventoryItemSlot[posX + i, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;
        Vector2 position = CalculatePositionOnGrid(posX, posY);

        rectTransform.localPosition = position;
    }

    public Vector2 CalculatePositionOnGrid(int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * titleSizeWidth;
        position.y = -(posY * titleSizeHeigth);
        return position;
    }

    private bool OverlpaCheck(int posX, int posY, int width, int height, ref InventoryItem overlapitem)
    {
        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                if(inventoryItemSlot[posX+i,posY+y] != null)
                {
                    overlapitem = inventoryItemSlot[posX + i, posY + y];
                }
                else
                {
                    if(overlapitem != inventoryItemSlot[posX + i, posY + y])
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private bool CheckAvailablaSpace(int posX, int posY, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + i, posY + y] != null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    bool PositionCheck(int posX,int posY)
    {
        if(posX < 0 || posY < 0)
        {
            return false;
        }

        if(posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    public bool BoundryCheck(int posX, int posY, int width, int height)
    {
        if (!PositionCheck(posX, posY))
            return false;

        posX += width -1 ;
        posY += height - 1;

        if (!PositionCheck(posX, posY))
            return false;

        return true;
    }
}

