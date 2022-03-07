using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftBehaviour : ItemHelderBase, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public GameObject craftResult;
    private List<CraftRecipe> craftRecipes;
    private ItemDatabase itemDatabase;
    private GameObject craftResultObject;

    // Start is called before the first frame update
    void Start()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        itemDatabase = gameController.GetComponent<ItemDatabase>();
        craftRecipes = itemDatabase.recipes;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mainMenuBehaviour.SetMouseArea(MouseArea.CRAFT);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mainMenuBehaviour.SetMouseArea(null);
    }

    public override void AddItem(Item item, bool draggable = true, bool showAction = true)
    {
        base.AddItem(item);
        CheckForRecipes();
    }

    public override void RemoveItem(Item item)
    {
        base.RemoveItem(item);
        CheckForRecipes();
    }

    public void CheckForRecipes()
    {
        var names = string.Join("_", items.OrderBy(x => x.item.name).Select(x => $"{x.item.name}|{x.quantity}"));
        var results = craftRecipes.Where(x => x.GetRecipeName().Equals(names)).FirstOrDefault();
        if (results != null)
        {
            var itemFromDB = itemDatabase.GetByName(results.itemGenerated);
            var obj = GameObject.Instantiate(mainMenuBehaviour.itemPrefab);
            obj.GetComponent<PanelItemHandler>().Set(new Slot() { item = itemFromDB, quantity = results.itemQtdGenerated }, mainMenu,false,false);
            obj.transform.SetParent(this.craftResult.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
            obj.SetActive(true); //Activate the GameObject
            craftResultObject = obj;
        }
        else
        {
            GameObject.Destroy(craftResultObject);
        }
    }
}
