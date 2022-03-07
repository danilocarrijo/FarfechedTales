using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    public int cur_Level;
    public int baseXP = 20;
    public int cur_XP;

    public int xpForNextLevel;
    public int xpDifferenceNextLevel;
    public int totalXPDifference;

    public float fillAmount;
    public float reverseFillAmount;

    public int statPoints;
    public int skillPoints;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddXP",1f,1f); 
    }

    public void AddXP()
    {
        CalculateLevel(5);
    }

    void CalculateLevel(int amount)
    {
        cur_XP += amount;

        int temp_cur_level = (int)Mathf.Sqrt(cur_XP / baseXP) + 1;

        if(cur_Level != temp_cur_level)
        {
            cur_Level = temp_cur_level;
        }

        xpForNextLevel = baseXP * cur_Level * cur_Level;
        xpDifferenceNextLevel = xpForNextLevel - cur_XP;
        totalXPDifference = xpForNextLevel - (baseXP * (cur_Level - 1) * (cur_Level - 1));

        fillAmount = (float)xpDifferenceNextLevel / (float)totalXPDifference;
        reverseFillAmount = 1 - fillAmount;

        statPoints = 5 * (cur_Level - 1);
        skillPoints = 15 * (cur_Level - 1);
    }
}


