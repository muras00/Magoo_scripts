using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeadUpDisplay : MonoBehaviour
{
    public int currentLevel;
    public int currentExp;
    public int ExpToNextLevel;
    public int gold;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI goldText;

    public void Start()
    {
        levelText.text = "Level\n" + currentLevel;
        expText.text = "K " + currentExp + "/" + ExpToNextLevel;
        goldText.text = "P " + gold;
        currentLevel = 30;
        GetExp(200);
        //GetGold(50);
    }

    public void GetExp(int newExp)
    {
        int sum = currentExp + newExp;
        if (currentLevel == 50)
        {
            currentExp += newExp;
        }
        else if (ExpToNextLevel <= sum)
        {
            currentExp = sum - ExpToNextLevel;
            LevelUp(currentLevel);
        }
        else
        {
            currentExp += newExp;
        }
        expText.text = "K " + currentExp + "/" + ExpToNextLevel;
    }

    public void GetGold(int newGold)
    {
        gold += newGold;
        goldText.text = "P " + gold;
    }
    
    //What if Exp overflows?
    public void LevelUp(int level)
    {
        level++;
        if (level == 50)
        {
            ExpToNextLevel = 0;
        }
        else if (level < 20)
        {
            ExpToNextLevel = 100 + (10 * level);
        }
        else
        {
            ExpToNextLevel = 300 + (15 * (level - 20));
        }
        currentLevel = level;
        levelText.text = "Level\n" + currentLevel;
    }
}