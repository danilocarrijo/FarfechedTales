using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject textPrefab;

    [System.Obsolete]
    void Update()
    {
        if (this.gameObject.active)
        {
            this.transform.position = new Vector3(Input.mousePosition.x + 300, Input.mousePosition.y);
        }
    }

    public void Set(Item item)
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        var name = GameObject.Instantiate(textPrefab);

        name.transform.SetParent(this.transform, false);


        var description = GameObject.Instantiate(textPrefab);

        description.transform.SetParent(this.transform, false);


        name.GetComponent<Text>().text = item.name;

        description.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        description.GetComponent<Text>().text = item.description;

        foreach (var modifier in item.weaponModifiers)
        {
            if(modifier.dameType == DameType.NORMAL)
            {
                var tempTextBox = GameObject.Instantiate(textPrefab);

                tempTextBox.transform.SetParent(this.transform, false);

                tempTextBox.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

                tempTextBox.GetComponent<Text>().text = $"{modifier.minType} - {modifier.maxType} Damage";
            }
            else
            {
                var tempTextBox = GameObject.Instantiate(textPrefab);

                tempTextBox.transform.SetParent(this.transform, false);

                tempTextBox.GetComponent<Text>().color = Color.blue;

                tempTextBox.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

                tempTextBox.GetComponent<Text>().text = $"{modifier.minType} - {modifier.maxType} {modifier.dameType} Damage";
            }
        }
    }
}
