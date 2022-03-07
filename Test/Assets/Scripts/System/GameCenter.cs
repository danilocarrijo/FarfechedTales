using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCenter : MonoBehaviour
{
    [SerializeField]
    public GAMEDIFICULTY gameDificulty;

    [SerializeField]
    private GameObject commonVFX; 

    [SerializeField]
    private GameObject uncommonVFX;

    [SerializeField]
    private GameObject rareVFX;

    [SerializeField]
    private GameObject legendaryVFX;

    [SerializeField]
    private GameObject uniqueVFX;

    [HideInInspector]
    public GAMEAREA gameArea = GAMEAREA.GAME;

    [SerializeField]
    public GameObject mainMenu;

    [SerializeField]
    public GameObject descriptionMenu;

    [SerializeField]
    public int weaponModifierRangeEasy;

    [SerializeField]
    public int weaponModifierRangeMedium;

    [SerializeField]
    public int weaponModifierRangeHard;

    [SerializeField]
    public int weaponModifierRangeUltra;

    [SerializeField]
    public Vector2 weaponDamageRangeEasy;

    [SerializeField]
    public Vector2 weaponDamageRangeMedium;

    [SerializeField]
    public Vector2 weaponDamageRangeHard;

    [SerializeField]
    public Vector2 weaponDamageRangeUltra;


    [HideInInspector]
    public GameObject player;

    private static GameCenter _instance;

    public static GameCenter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameCenter>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(false);
        mainMenu.GetComponent<MainMenu>().Set();
        player.transform.GetChild(0).GetComponent<Moviment>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObjectWorldRange(Vector3 location,float range, Item item,bool generateAtributes = false)
    {
        float xPosition = Random.Range(location.x - range, location.x + range);
        float zPosition = Random.Range(location.z - range, location.z + range);

        var randomPosition = new Vector3(xPosition, location.y + 0.2f, zPosition);

        SpawnObjectWorld(randomPosition, item, generateAtributes);
    }

    public void SpawnObjectWorld(Vector3 location,Item item, bool generateAtributes = false)
    {
        GameObject ret;
        switch (item.rarity)
        {
            case ItemRarity.COMMON:
                ret = GameObject.Instantiate(commonVFX);
                ret.transform.position = location;
                break;
            case ItemRarity.UNCOMMON:
                ret = GameObject.Instantiate(uncommonVFX);
                ret.transform.position = location;
                break;
            case ItemRarity.RARE:
                ret = GameObject.Instantiate(rareVFX);
                ret.transform.position = location;
                break;
            case ItemRarity.LEGANDARY:
                ret = GameObject.Instantiate(legendaryVFX);
                ret.transform.position = location;
                break;
            case ItemRarity.UNIQUE:
                ret = GameObject.Instantiate(uniqueVFX);
                ret.transform.position = location;
                break;
            default:
                ret = GameObject.Instantiate(commonVFX);
                ret.transform.position = location;
                break;
        }

        SpawnWeapon(ret, item, generateAtributes);

    }

    void SpawnWeapon(GameObject ret,Item _item, bool generateAtributes = false)
    {
        Item item = (Item)_item.Clone();
        ret.GetComponent<ItemBehaviour>().item = item;
        if(generateAtributes)
            item.weaponModifiers = WeponModifierByDificulty(item);

        if (item.itemPrefab != null)
        {
            var itemPrefab = GameObject.Instantiate(item.itemPrefab);

            itemPrefab.transform.SetParent(ret.transform);
            itemPrefab.transform.localScale = Vector3.one;
            itemPrefab.transform.position = ret.transform.position;
            itemPrefab.transform.localRotation = Quaternion.identity;
        }
    }

    List<WeaponMidifier> WeponModifierByDificulty(Item item)
    {
        List<WeaponMidifier> ret = new List<WeaponMidifier>();
        ret.AddRange(item.weaponModifiers);

        var max = 1;
        var damage = new Vector2(1,1);

        switch (gameDificulty)
        {
            case GAMEDIFICULTY.EASY:
                max = weaponModifierRangeEasy;
                damage = weaponDamageRangeEasy;
                break;
            case GAMEDIFICULTY.MEDIUM:
                max = weaponModifierRangeMedium;
                damage = weaponDamageRangeMedium;
                break;
            case GAMEDIFICULTY.HARD:
                max = weaponModifierRangeHard;
                damage = weaponDamageRangeHard;
                break;
            case GAMEDIFICULTY.ULTRA:
                max = weaponModifierRangeUltra;
                damage = weaponDamageRangeUltra;
                break;
        }

        var maxModifiers = Random.Range(1, max);
        for (int i = 0; i < maxModifiers; i++)
        {
            WeaponMidifier obj = new WeaponMidifier();
            obj.dameType = (DameType)Random.Range(2, 6);
            obj.minType = (int)Random.Range(1, damage.x);
            obj.maxType = (int)Random.Range(1 + obj.minType, damage.y + obj.minType);
            ret.Add(obj);
        }

        return ret;
    } 
}

public enum GAMEDIFICULTY
{
    EASY,
    MEDIUM,
    HARD,
    ULTRA
}

public enum GAMEAREA
{
    GAME,
    MENU,
    CHARACTER,
    INVENTORY
}
