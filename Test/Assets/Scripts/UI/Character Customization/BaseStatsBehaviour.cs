using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseStatsBehaviour : MonoBehaviour
{
    private PlayerBehavor playerBehavor;

    private Text field;

    public BaseStats_Enum status;

    // Start is called before the first frame update
    void Start()
    {
        if (CharacterCustomizationBehaviour.Instance.character != null)
            playerBehavor = CharacterCustomizationBehaviour.Instance.character.GetComponent<PlayerBehavor>();
        field = this.gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (playerBehavor == null)
        {
            playerBehavor = CharacterCustomizationBehaviour.Instance.character.GetComponent<PlayerBehavor>();
        }
        if (playerBehavor != null && field != null)
        {
            switch (status)
            {
                case BaseStats_Enum.STENGTH:
                    field.text = playerBehavor.infos.strenhtg.ToString();
                    break;
                case BaseStats_Enum.CHARISMA:
                    field.text = playerBehavor.infos.charisma.ToString();
                    break;
                case BaseStats_Enum.ENDURANCE:
                    field.text = (playerBehavor.infos.endurance + playerBehavor.infos.strenhtg).ToString();
                    break;
                case BaseStats_Enum.EVADE:
                    field.text = (playerBehavor.infos.evade + playerBehavor.infos.dexterity).ToString();
                    break;
                case BaseStats_Enum.INTELIGENCE:
                    field.text = playerBehavor.infos.inteligence.ToString();
                    break;
                case BaseStats_Enum.WISDOM:
                    field.text = playerBehavor.infos.wisdom.ToString();
                    break;
                case BaseStats_Enum.WILL:
                    field.text = (playerBehavor.infos.will + playerBehavor.infos.wisdom).ToString();
                    break;
                case BaseStats_Enum.DEXTERITY:
                    field.text = playerBehavor.infos.dexterity.ToString();
                    break;
                default:
                    break;
            }
        }
    }

    public void Remove(int amount)
    {
        switch (status)
        {
            case BaseStats_Enum.STENGTH:
                if (playerBehavor.infos.strenhtg > 0 )
                {
                    playerBehavor.infos.strenhtg += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.CHARISMA:
                if (playerBehavor.infos.charisma > 0)
                {
                    playerBehavor.infos.charisma += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.ENDURANCE:
                if (playerBehavor.infos.endurance > 0 )
                {
                    playerBehavor.infos.endurance += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.EVADE:
                if (playerBehavor.infos.evade > 0 )
                {
                    playerBehavor.infos.evade += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.INTELIGENCE:
                if (playerBehavor.infos.inteligence > 0 )
                {
                    playerBehavor.infos.inteligence += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.WISDOM:
                if (playerBehavor.infos.wisdom > 0 )
                {
                    playerBehavor.infos.wisdom += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.WILL:
                if (playerBehavor.infos.wisdom > 0)
                {
                    playerBehavor.infos.will += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.DEXTERITY:
                if (playerBehavor.infos.wisdom > 0)
                {
                    playerBehavor.infos.dexterity += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            default:
                break;
        }
    }
    public void Add(int amount)
    {
        switch (status)
        {
            case BaseStats_Enum.STENGTH:
                if ( CharacterCustomizationBehaviour.Instance.baseStatsPoints > 0)
                {
                    playerBehavor.infos.strenhtg += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.CHARISMA:
                if ( CharacterCustomizationBehaviour.Instance.baseStatsPoints > 0)
                {
                    playerBehavor.infos.charisma += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.ENDURANCE:
                if ( CharacterCustomizationBehaviour.Instance.baseStatsPoints > 0)
                {
                    playerBehavor.infos.endurance += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.EVADE:
                if ( CharacterCustomizationBehaviour.Instance.baseStatsPoints > 0)
                {
                    playerBehavor.infos.evade += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.INTELIGENCE:
                if ( CharacterCustomizationBehaviour.Instance.baseStatsPoints > 0)
                {
                    playerBehavor.infos.inteligence += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.WISDOM:
                if ( CharacterCustomizationBehaviour.Instance.baseStatsPoints > 0)
                {
                    playerBehavor.infos.wisdom += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.WILL:
                if (CharacterCustomizationBehaviour.Instance.baseStatsPoints > 0)
                {
                    playerBehavor.infos.will += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            case BaseStats_Enum.DEXTERITY:
                if (CharacterCustomizationBehaviour.Instance.baseStatsPoints > 0)
                {
                    playerBehavor.infos.dexterity += amount;
                    CharacterCustomizationBehaviour.Instance.baseStatsPoints += amount * -1;
                }
                break;
            default:
                break;
        }
    }
}
