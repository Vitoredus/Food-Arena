using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class HelpfulFunctions
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void SaveStatus(PlayerClass characterStats)
    {
        PlayerPrefs.SetString(characterStats.name, characterStats.name);
        PlayerPrefs.SetInt("HP" + characterStats.name, characterStats.hp);
        PlayerPrefs.SetInt("Exp" + characterStats.name, characterStats.experience);
        PlayerPrefs.SetInt("Level" + characterStats.name, characterStats.level);
        PlayerPrefs.SetInt("ExpToNextLvl" + characterStats.name, characterStats.experienceToNextLevel);
        PlayerPrefs.SetInt("ExpPoints" + characterStats.name, characterStats.experiencePoints);
        PlayerPrefs.SetInt("Ingredients", PlayerClass.ingredients);
        //Debug.Log("Salvou");
    }

    public static void LoadStatus(PlayerClass characterStats)
    {
        if (PlayerPrefs.HasKey(characterStats.name))
        {
            characterStats.name = PlayerPrefs.GetString(characterStats.name);
            characterStats.hp = PlayerPrefs.GetInt("HP" + characterStats.name);
            characterStats.experience = PlayerPrefs.GetInt("Exp" + characterStats.name);
            characterStats.level = PlayerPrefs.GetInt("Level" + characterStats.name);
            characterStats.experienceToNextLevel = PlayerPrefs.GetInt("ExpToNextLvl" + characterStats.name);
            characterStats.experiencePoints = PlayerPrefs.GetInt("ExpPoints" + characterStats.name);
            PlayerClass.ingredients = PlayerPrefs.GetInt("Ingredients");
            //Debug.Log("Loudou");
        }
    }

}
