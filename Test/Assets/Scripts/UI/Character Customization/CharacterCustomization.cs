using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{


    [SerializeField]
    public GameObject prefab;

    [SerializeField]
    public string socketName;

    [SerializeField]
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSkinColor()
    {
        var socket = GameObject.FindGameObjectWithTag(socketName);
        var material = socket.GetComponentInChildren<Renderer>().materials.Where(x => x.name.Contains("N00_000_00_Face_00_SKIN (Instance)"));
        foreach (var item in material)
        {
            item.SetColor("_BaseColor", color);
        }
        var player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).Find("Body");
        material = player.GetComponentInChildren<Renderer>().materials.Where(x => x.name.Contains("00_000_00_Body_00_SKIN"));
        foreach (var item in material)
        {
            item.SetColor("_BaseColor", color);
        }
    }

    public void Set()
    {
        var socket = GameObject.FindGameObjectWithTag(socketName);
        foreach (Transform child in socket.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        var obj = GameObject.Instantiate(prefab);
        obj.SetActive(true);
        obj.transform.SetParent(socket.transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.position = socket.transform.position;
        obj.transform.localRotation = Quaternion.identity;
        Material material = obj.GetComponentInChildren<Renderer>().materials.Where(x => x.name.Contains("EyeIris_00_EYE")).FirstOrDefault();
        if(material != null)
        {
            CharacterCustomizationBehaviour.Instance.currFace = obj;
        }
        material = obj.GetComponentInChildren<Renderer>().materials.Where(x => x.name.Contains("Hair")).FirstOrDefault();
        if (material != null)
        {
            CharacterCustomizationBehaviour.Instance.currHair = obj;
        }
    }
}
