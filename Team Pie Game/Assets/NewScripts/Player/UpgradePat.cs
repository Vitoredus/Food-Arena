using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePat : MonoBehaviour
{
    [Header("Estrelas")]
    public GameObject[] estrelasRoupa = new GameObject[5];
    public GameObject[] estrelasArma = new GameObject[3];

    [Header("Botoes")]
    public Button buttonRoupa;
    public Button buttonArma;

    [Header("Textos")]
    public Text textButtonRoupa;
    public Text textButtonArma;
    public Text textLvlRoupa; 
    public Text textLvlArma;

    private int custoPorUpgradeParaRoupa = 100;
    private int custoPorUpgradeParaArma = 100;

    private float danoExtra = 0;

    private int nivelDaRoupa = 1;
    private int nivelDaArma = 1;

    private int nivelDeEstrelaDaRoupa = 10;
    private int nivelDeEstrelaDaArma = 10;

    public AudioSource buySound;

    private void Start()
    {
        if (PlayerPrefs.HasKey("UpgradeRoupa")) custoPorUpgradeParaRoupa = PlayerPrefs.GetInt("UpgradeRoupa");

        if (PlayerPrefs.HasKey("UpgradeArma")) custoPorUpgradeParaArma = PlayerPrefs.GetInt("UpgradeArma");

        if (PlayerPrefs.HasKey("+DanoPorUpgrade")) danoExtra = PlayerPrefs.GetFloat("+DanoPorUpgrade");

        if (PlayerPrefs.HasKey("NivelDeEstrelaDaRoupa")) nivelDeEstrelaDaRoupa = PlayerPrefs.GetInt("NivelDeEstrelaDaRoupa");

        if (PlayerPrefs.HasKey("NivelDaRoupa")) nivelDaRoupa = PlayerPrefs.GetInt("NivelDaRoupa");

        if (PlayerPrefs.HasKey("NivelDeEstrelaDaArma")) nivelDeEstrelaDaArma = PlayerPrefs.GetInt("NivelDeEstrelaDaArma");

        if (PlayerPrefs.HasKey("NivelDaArma")) nivelDaArma = PlayerPrefs.GetInt("NivelDaArma");

        if(nivelDeEstrelaDaRoupa > 50)
        {
            buttonRoupa.interactable = false;
            textButtonRoupa.text = "FULL";
            textLvlRoupa.text = "LVL 50 / 50 (+ HP)";
        }
        else
        {
            textButtonRoupa.text = "" + custoPorUpgradeParaRoupa;
            textLvlRoupa.text = "LVL " + nivelDaRoupa + " / " + nivelDeEstrelaDaRoupa + " (+ HP)";
            estrelasRoupa[(nivelDeEstrelaDaRoupa / 10) - 1].SetActive(true);
        }

        if (nivelDeEstrelaDaArma > 30)
        {
            buttonArma.interactable = false;
            textButtonArma.text = "FULLY UPGRADED";
            textLvlArma.text = "LVL 30 / 30 (+ DPS)";
        }
        else
        {
            textButtonArma.text = "" + custoPorUpgradeParaArma;
            textLvlArma.text = "LVL " + nivelDaArma + " / " + nivelDeEstrelaDaArma + " (+ DPS)";
            estrelasArma[(nivelDeEstrelaDaArma / 10) - 1].SetActive(true);
        }


    }

    public void UpagradeCloth()
    {
        if(PlayerClass.ingredients >= custoPorUpgradeParaRoupa)
        {
            PlayerClass.ingredients -= custoPorUpgradeParaRoupa;
            custoPorUpgradeParaRoupa += 100;
            PlayerPrefs.SetInt("UpgradeRoupa", custoPorUpgradeParaRoupa);
            SettingsMenu.Instance.selectedCharacter.hp += 5;
            textButtonRoupa.text = "" + custoPorUpgradeParaRoupa;
            nivelDaRoupa += 1;

            if(nivelDaRoupa > nivelDeEstrelaDaRoupa)
            {
                nivelDaRoupa = 1;
                nivelDeEstrelaDaRoupa += 10;
                PlayerPrefs.SetInt("NivelDeEstrelaDaRoupa", nivelDeEstrelaDaRoupa);
            }

            if (nivelDeEstrelaDaRoupa > 50)
            {
                buttonRoupa.interactable = false;
                textButtonRoupa.text = "FULLY UPGRADED";
                textLvlRoupa.text = "LVL 50 / 50 (+ HP)";
                nivelDeEstrelaDaRoupa = 61;
                PlayerPrefs.SetInt("NivelDeEstrelaDaRoupa", nivelDeEstrelaDaRoupa);
            }
            else
            {
                estrelasRoupa[(nivelDeEstrelaDaRoupa / 10) - 1].SetActive(true);
                textLvlRoupa.text = "LVL " + nivelDaRoupa + " / " + nivelDeEstrelaDaRoupa + " (+ HP)";
            }

            PlayerPrefs.SetInt("NivelDaRoupa", nivelDaRoupa);
            SettingsMenu.Instance.UpdtadeUI();
            buySound.Play();
        }
    }

    public void UpgradeWeapon()
    {
        if (PlayerClass.ingredients >= custoPorUpgradeParaArma)
        {
            PlayerClass.ingredients -= custoPorUpgradeParaArma;
            custoPorUpgradeParaArma += 100;
            PlayerPrefs.SetInt("UpgradeArma", custoPorUpgradeParaArma);
            textButtonArma.text = "" + custoPorUpgradeParaArma;
            danoExtra += 0.5f;
            PlayerPrefs.SetFloat("+DanoPorUpgrade", danoExtra);
            nivelDaArma += 1;

            if (nivelDaArma > nivelDeEstrelaDaArma)
            {
                nivelDaArma = 1;
                nivelDeEstrelaDaArma += 10;
                PlayerPrefs.SetInt("NivelDeEstrelaDaArma", nivelDeEstrelaDaArma);

            }

            if (nivelDeEstrelaDaArma > 30)
            {
                buttonArma.interactable = false;
                textButtonArma.text = "FULLY UPGRADED";
                textLvlArma.text = "LVL 30 / 30 (+ DPS)";
                nivelDeEstrelaDaArma = 41;
                PlayerPrefs.SetInt("NivelDeEstrelaDaArma", nivelDeEstrelaDaArma);
            }
            else
            {
                estrelasArma[(nivelDeEstrelaDaArma / 10) - 1].SetActive(true);
                textLvlArma.text = "LVL " + nivelDaArma + " / " + nivelDeEstrelaDaArma + " (+ DPS)";
            }

            PlayerPrefs.SetInt("NivelDaArma", nivelDaArma);
            SettingsMenu.Instance.UpdtadeUI();
            buySound.Play();
        }
        SettingsMenu.Instance.UpdtadeUI();
    }
}
