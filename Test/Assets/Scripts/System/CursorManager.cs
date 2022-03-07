using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{

    
    private GameObject descriptionMenu;

    private string hitingTag;
    private GameCenter gameCenter;

    [System.Serializable]
    public class TheCursor {
        public string tag;
        public Texture2D cursorTexture;
    }

    public List<TheCursor> cursorList = new List<TheCursor>();

    // Start is called before the first frame update
    void Start()
    {
        gameCenter = GameCenter.Instance;
        descriptionMenu = gameCenter.descriptionMenu;
        //SetCursortexture(cursorList[0].cursorTexture);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        hitingTag = "";
        if (gameCenter.gameArea == GAMEAREA.GAME && Physics.Raycast(ray,out hit, 1000) && !EventSystem.current.IsPointerOverGameObject())
        {
            foreach (var item in cursorList)
            {
                hitingTag = hit.collider.tag;
                if (hitingTag.Equals("Item"))
                {
                    descriptionMenu.SetActive(true);
                    descriptionMenu.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - 110);
                    descriptionMenu.GetComponent<DescriptionMenu>().Set(hit.transform.parent.GetComponent<ItemBehaviour>().item);
                }
                else
                {
                    descriptionMenu.SetActive(false);
                }
                if (hit.collider.tag == item.tag)
                {
                    //SetCursortexture(item.cursorTexture);
                    return;
                }
            }
        }
        //SetCursortexture(cursorList[0].cursorTexture);
    }

    void SetCursortexture(Texture2D tex)
    {
        Cursor.SetCursor(tex,Vector2.zero,CursorMode.Auto);
    }
}
