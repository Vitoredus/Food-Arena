using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoSingleton<SettingsMenu>
{
    public Image loadingScreen;

    public Text ingredientes;
    public Text ingredientesUpgrade;
    public AudioMixer audioMixer;

    public Slider expBar;
    public Text levelText;

    public Text expPonts;

    public PlayerClass selectedCharacter = new PlayerClass();

    private void Start()
    {
        StartCoroutine(FadeOut());
        PlayerClass.ingredients = PlayerPrefs.GetInt("Ingredients");
        HelpfulFunctions.LoadStatus(selectedCharacter);
        UpdtadeUI();
    }

    public void UpdtadeUI()
    {
        expBar.value = (float)selectedCharacter.experience / selectedCharacter.experienceToNextLevel;
        levelText.text = "Level: " + (selectedCharacter.level + 1);
        expPonts.text = "Skill points available:  " + selectedCharacter.experiencePoints;
        ingredientes.text = "" + PlayerClass.ingredients;
        ingredientesUpgrade.text = "" + PlayerClass.ingredients;
        HelpfulFunctions.SaveStatus(selectedCharacter);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        var tempColor = loadingScreen.color;

        while (true)
        {
            yield return null;
            if(tempColor.a > 0f)
            {
                tempColor.a -= Time.deltaTime;
            }
            else
            {
                loadingScreen.enabled = false;
                break;
            }
            loadingScreen.color = tempColor;
        }
        
    }
}
