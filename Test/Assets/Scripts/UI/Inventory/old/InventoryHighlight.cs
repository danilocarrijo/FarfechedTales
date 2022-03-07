using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter; 

    public void Show(bool show)
    {
        highlighter.gameObject.SetActive(show);
    }

    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.itemData.width * ItemGrid.titleSizeWidth;
        size.y = targetItem.itemData.height * ItemGrid.titleSizeHeigth;

        highlighter.sizeDelta = size;
    }

    public void SetPosition(ItemGrid targetGrid,InventoryItem targetItem)
    {

        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem.onGridPositionX, targetItem.onGridPositionY);

        highlighter.localPosition = pos;


    }

    public void SetParent(ItemGrid targetGrid)
    {
        if (targetGrid == null)
            return;
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
    }

    public void SetPosition(ItemGrid targetGrid,InventoryItem targetItem, int posX,int posY)
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(posX, posY);

        highlighter.localPosition = pos;
    }
}
