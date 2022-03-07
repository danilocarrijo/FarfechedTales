using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slotbehaviour : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler
{
    private DragHandler canvas;
    [SerializeField]
    public SlotType slotType;
    [SerializeField]
    public Sprite commonBackground;
    [SerializeField]
    public Sprite uncommonBackground;
    [SerializeField]
    public Sprite rareBackground;
    [SerializeField]
    public Sprite legendaryBackground;
    [SerializeField]
    public Sprite uniqueBackground;
    private GameObject descriptionMenu;

    private Sprite defaultSprite;


    private Item _item;
    public Item item { 
        get {
            return _item;
        } 
        set {
            _item = value;
            SetItem();
        } 
    }
    public int position;
    public GameObject itemPrefab;

    private GameObject currItem;

    void Start()
    {
        descriptionMenu = GameObject.FindGameObjectWithTag("DescriptionMenu");
        defaultSprite = this.transform.GetChild(0).GetComponent<Image>().sprite;
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<DragHandler>();
    }

    void SetItem()
    {
        if(_item != null)
        {
            currItem = GameObject.Instantiate(itemPrefab);
            currItem.transform.Find("Icon").GetComponent<Image>().sprite = _item.icon;
            currItem.GetComponent<PanelItemHandler>().item = _item;
            currItem.transform.SetParent(this.transform);
            currItem.transform.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            currItem.transform.GetComponent<RectTransform>().anchorMax = Vector2.one;
            currItem.transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            currItem.transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            switch (_item.rarity)
            {
                case ItemRarity.COMMON:
                    this.transform.GetChild(0).GetComponent<Image>().sprite = commonBackground;
                    break;
                case ItemRarity.UNCOMMON:
                    this.transform.GetChild(0).GetComponent<Image>().sprite = uncommonBackground;
                    break;
                case ItemRarity.RARE:
                    this.transform.GetChild(0).GetComponent<Image>().sprite = rareBackground;
                    break;
                case ItemRarity.LEGANDARY:
                    this.transform.GetChild(0).GetComponent<Image>().sprite = legendaryBackground;
                    break;
                case ItemRarity.UNIQUE:
                    this.transform.GetChild(0).GetComponent<Image>().sprite = uniqueBackground;
                    break;
                default:
                    break;
            }
            currItem.SetActive(true);
        }
        else
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = defaultSprite;
            GameObject.Destroy(currItem);
        }
    }

    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(descriptionMenu != null && _item != null)
        {
            descriptionMenu.GetComponent<DescriptionMenu>().Set(_item);
            descriptionMenu.SetActive(true);
        }
        canvas.currSlot = this;
    }

    [System.Obsolete]
    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionMenu != null )
        {
            descriptionMenu.SetActive(false);
        }
        canvas.currSlot = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas.currDragSlot = this;
        canvas.originDragSlot = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvas.currGameArea != null)
        {
            if (canvas.currSlot != null)
                canvas.currSlot.HanldeOnEndDrag();
            else
            {
                canvas.currDragSlot = null;
            }
        }
        else
        {
            var location = GameCenter.Instance.player.transform.localPosition;
            GameCenter.Instance.SpawnObjectWorldRange(location, 1.5f, canvas.currDragSlot.item);
            switch (canvas.currDragSlot.slotType)
            {
                case SlotType.ITEM:
                    GameCenter.Instance.player.GetComponent<PlayerBehavor>().RemoveItem(canvas.currDragSlot.item);
                    break;
                case SlotType.CHAR_WEAPON:
                    canvas.currDragSlot.item = null;
                    GameCenter.Instance.player.GetComponent<PlayerBehavor>().UnequilWeapon();
                    break;
                default:
                    break;
            }
            canvas.currDragSlot = null;
        }
    }

    private void HanldeOnEndDrag()
    {
        switch (slotType)
        {
            case SlotType.ITEM:
                HandleItemSlot();
                break;
            case SlotType.CHAR_WEAPON:
                HandleWeaponSlot();
                break;
            default:
                break;
        }
        canvas.currDragSlot = null;
    }

    private void HandleItemSlot()
    {
        if(canvas.currDragSlot.slotType == SlotType.ITEM)
            GameCenter.Instance.player.GetComponent<PlayerBehavor>().SwapSlot(canvas.originDragSlot, position);
        if (canvas.currDragSlot.slotType == SlotType.CHAR_WEAPON)
        {
            this.item = canvas.currDragSlot.item;
            canvas.currDragSlot.item = null;
            GameCenter.Instance.player.GetComponent<PlayerBehavor>().AddItem(this.item);
            GameCenter.Instance.player.GetComponent<PlayerBehavor>().UnequilWeapon();
        }
    }

    private void HandleWeaponSlot()
    {
        if (canvas.currDragSlot.slotType == SlotType.ITEM && canvas.currDragSlot.item.type == ItemType.WEAPON )
        {
            this.item = canvas.currDragSlot.item;
            GameCenter.Instance.player.GetComponent<PlayerBehavor>().RemoveItem(canvas.currDragSlot.item);
            GameCenter.Instance.player.GetComponent<PlayerBehavor>().EquilWeapon(this.item);
        }
    }
}
