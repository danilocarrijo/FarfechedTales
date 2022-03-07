using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [SerializeField]
    public List<Item> items;
    [SerializeField]
    public List<CraftRecipe> recipes; 

    public Item GetByName(string name)
    {
        return items.Where(x => x.name.Equals(name)).FirstOrDefault();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
