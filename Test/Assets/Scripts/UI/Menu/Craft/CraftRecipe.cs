using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CraftRecipe 
{
    [SerializeField]
    public List<CraftRecipeItem> items;
    [SerializeField]
    public string itemGenerated;
    [SerializeField]
    public int itemQtdGenerated;

    public string GetRecipeName()
    {
        return string.Join("_", items.OrderBy(x => x.itemName).Select(x => $"{x.itemName}|{x.itemQtd}"));
    }
}

[System.Serializable]
public class CraftRecipeItem
{

    [SerializeField]
    public string itemName;
    [SerializeField]
    public int itemQtd;
}
