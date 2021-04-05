using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpScreen : MonoBehaviour
{
    private Slider expBar;
    private Text levelText;

    public Animator AM;

    [SerializeField] private Text ganhouExpPoint;

    private int currentLevel = 0;
    private int currentExperience = 0;
    private int currentExperienceToNextLevel = 100;

    private PlayerStatus playerStatus;
    private PlayerClass playerClass;

    private bool animateExpBar = false;

    void Awake()
    {
        expBar = transform.Find("ExpBar").GetComponent<Slider>();
        levelText = transform.Find("LevelText").GetComponent<Text>();
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    private void OnEnable()
    {
        playerClass = playerStatus.GetPlayerClass();
        currentExperience = playerClass.experience;
        currentLevel = playerClass.level;
        currentExperienceToNextLevel = playerClass.experienceToNextLevel;
        UpdateUI();
        AddExperience(PlayerManager.instance.GetExperienceRuntime());
    }

    void UpdateUI()
    {
        expBar.value = (float)currentExperience/ currentExperienceToNextLevel;
        levelText.text = "Level: " + (currentLevel+1);
    }

    public void AddExperience(int amount)
    {
        playerClass.experience += amount;
        while (playerClass.experience >= playerClass.experienceToNextLevel)
        {
            playerClass.level++;
            if((playerClass.level + 1) %5 == 0)
            {
                playerClass.experiencePoints += 1;
            }

            playerClass.experience -= playerClass.experienceToNextLevel;
            playerClass.experienceToNextLevel += 20; 
        }
        animateExpBar = true;
    }

    void Update()
    {
        if(animateExpBar)
        {
            if(currentLevel < playerClass.level)
            {
                ExpAdd();
            }
            else
            {
                if(currentExperience < playerClass.experience)
                {
                    ExpAdd();
                }
                else
                {
                    animateExpBar = false;
                }
            }
            UpdateUI();
        }
    }

    void ExpAdd()
    {
        currentExperience++;
        if(currentExperience > currentExperienceToNextLevel)
        {
            currentLevel++;
            if ((currentLevel + 1) % 5 == 0)
            {
                ganhouExpPoint.text = "You won +1 Skill Point";
            }
            currentExperience -= currentExperienceToNextLevel;
            currentExperienceToNextLevel += 20;
        }
    }

    public void SairDaCena(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }
    IEnumerator LoadScene(string sceneName)
    {
        AM.SetTrigger("Ficar grande");
        yield return new WaitForSeconds(1.5f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().Save();
        HelpfulFunctions.LoadScene(sceneName);
    }
}
