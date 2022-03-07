using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCustomizationBehaviour : MonoBehaviour
{
    private static CharacterCustomizationBehaviour _instance;

    [HideInInspector]
    public GameObject currFace;

    [HideInInspector]
    public GameObject currHair;

    [SerializeField]
    public FlexibleColorPicker hairColorPicker;

    [SerializeField]
    public FlexibleColorPicker eyeColorPicker;

    [SerializeField]
    public GameObject maleModel;

    [SerializeField]
    public GameObject femaleModel;
    [SerializeField]
    public GameObject defaultMaleHair;
    [SerializeField]
    public GameObject defaultMaleFace;
    [SerializeField]
    public GameObject defaultFemaleHair;
    [SerializeField]
    public GameObject defaultFemaleFace;
    [SerializeField]
    public string socketNameHair;
    [SerializeField]
    public string socketNameFace;
    [SerializeField]
    public GameObject tooltipgameObject;
    [SerializeField]
    public GameObject baseStatsPointsObj;
    [SerializeField]
    public GameObject traitPrefab;
    [SerializeField]
    public List<Trait> traitDataBae;
    [SerializeField]
    public GameObject traitPanel;
    [SerializeField]
    public GameObject traitTooltip;
    [SerializeField]
    public List<Skill> skillDataBase;
    [SerializeField]
    public GameObject skillPanel;
    [SerializeField]
    public GameObject skillooltip;
    [HideInInspector]
    public List<Ambition> ambitionsDataBae;
    [SerializeField]
    public GameObject ambitionsPanel;
    [SerializeField]
    public GameObject ambitionsTooltip;
    [SerializeField]
    public GameObject ambitionsTooltipActivityList;
    public GameObject ambitionsToolTaskPrefab;
    public List<AmbitionIcon> ambitionsIconDataBae;
    [HideInInspector]
    public Color skinColor;

    public int maxTraits;
    public int maxSkills;
    public int maxAmbitions;

    private Text baseStatsPointsText;

    [SerializeField]
    public AnimatorController baseAnimator;

    public int baseStatsPoints = 10;
    [HideInInspector]
    public int totalBaseStatsPoints = 0;

    [HideInInspector]
    public GameObject character;

    public static CharacterCustomizationBehaviour Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CharacterCustomizationBehaviour>();
            }

            return _instance;
        }
    }

    private void Start()
    {
        skinColor = new Color(236, 188, 180);
        var monsterHunter = new MonsterHunter();
        monsterHunter.icon = ambitionsIconDataBae.Where(x => x.name == monsterHunter.name).FirstOrDefault()?.icon;
        ambitionsDataBae.Add(monsterHunter);
        character = GameObject.FindGameObjectWithTag("Player");
        SetCharacterGender("Male");
        baseStatsPointsText = baseStatsPointsObj.GetComponent<Text>();
        totalBaseStatsPoints = baseStatsPoints;
        foreach (var item in traitDataBae)
        {
            var obj = GameObject.Instantiate(traitPrefab);
            obj.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
            obj.transform.GetComponent<TraitTooltip>().trait = item;
            obj.transform.GetComponent<AmbitionsTooltip>().enabled = false;
            obj.transform.GetComponent<SkillTooltip>().enabled = false;
            obj.transform.SetParent(traitPanel.transform);
        }
        foreach (var item in skillDataBase)
        {
            var obj = GameObject.Instantiate(traitPrefab);
            obj.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
            obj.transform.GetComponent<SkillTooltip>().skill = item;
            obj.transform.GetComponent<AmbitionsTooltip>().enabled = false;
            obj.transform.GetComponent<TraitTooltip>().enabled = false;
            obj.transform.SetParent(skillPanel.transform);
        }
        foreach (var item in ambitionsDataBae)
        {
            var obj = GameObject.Instantiate(traitPrefab);
            obj.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
            obj.transform.GetComponent<AmbitionsTooltip>().ambition = item;
            obj.transform.GetComponent<SkillTooltip>().enabled = false;
            obj.transform.GetComponent<TraitTooltip>().enabled = false;
            obj.transform.SetParent(ambitionsPanel.transform);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("teste");
    }

    public IEnumerator SetFaceHairDefault(GameObject _defaultFace, GameObject _defaultHair)
    {

        yield return new WaitForSeconds(0.01f);
        var socket = GameObject.FindGameObjectWithTag(socketNameFace);
        var obj = GameObject.Instantiate(_defaultFace);
        obj.SetActive(true);
        obj.transform.SetParent(socket.transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.position = socket.transform.position;
        obj.transform.localRotation = Quaternion.identity;
        currFace = obj;
        socket = GameObject.FindGameObjectWithTag(socketNameHair);
        obj = GameObject.Instantiate(_defaultHair);
        obj.SetActive(true);
        obj.transform.SetParent(socket.transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.position = socket.transform.position;
        obj.transform.localRotation = Quaternion.identity;
        currHair = obj;
    }

    public void SetCharacterGender(string gender)
    {
        foreach (Transform child in character.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (gender.Equals("Male"))
        {

            var obj = GameObject.Instantiate(maleModel);
            obj.SetActive(true);
            obj.transform.SetParent(character.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.position = character.transform.position;
            obj.transform.localRotation = Quaternion.identity;
            obj.GetComponent<Animator>().runtimeAnimatorController = baseAnimator;
            StartCoroutine(SetFaceHairDefault(defaultMaleFace,defaultMaleHair));
        }
        else
        {

            var obj = GameObject.Instantiate(femaleModel);
            obj.SetActive(true);
            obj.transform.SetParent(character.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.position = character.transform.position; 
            obj.transform.localRotation = Quaternion.identity;
            obj.GetComponent<Animator>().runtimeAnimatorController = baseAnimator;
            StartCoroutine(SetFaceHairDefault(defaultFemaleFace,defaultFemaleHair));
        }
    }

    public void ResetAllAnimatorTriggers(Animator animator)
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }
    }

    private void Update()
    {
        if(baseStatsPointsText != null)
        {
            baseStatsPointsText.text = baseStatsPoints.ToString();
        }
        if (currFace != null)

        {
            Material material = currFace.GetComponentInChildren<Renderer>().materials.Where(x => x.name.Contains("EyeIris_00_EYE")).FirstOrDefault();
            material.SetColor("_BaseColor", eyeColorPicker.color);
        }
        if (currHair != null)
        {

            Material material = currHair.GetComponentInChildren<Renderer>().materials.Where(x => x.name.Contains("Hair")).FirstOrDefault();
            material.SetColor("_BaseColor", hairColorPicker.color);
        }
    }
}
