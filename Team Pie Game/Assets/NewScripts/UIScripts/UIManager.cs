using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject adSkippedScreen;

    public GameObject adButton;
    public bool AnuncioJaUsado { get; private set; }

    public DeathScreen deathScreen;
    public ExpScreen expScreen;

    public Text ingredientes;

    public Action reviverPlayer;

    public GameObject pauseScreen;

    private void Start()
    {
        UpDateIngredientText();
    }

    public void ActiveExpScreen()
    {
        adSkippedScreen.SetActive(false);
        deathScreen.gameObject.SetActive(false);
        expScreen.gameObject.SetActive(true);
    }

    public void DeathScreenControl(bool result)
    {
        deathScreen.gameObject.SetActive(result);
        if (AnuncioJaUsado)
            adButton.SetActive(false);
    }

    public void AdShow()
    {
        AnuncioJaUsado = true;
        reviverPlayer?.Invoke();
    }

    public void AdSkipped()
    {
        adSkippedScreen.SetActive(true);
    }

    public void UpDateIngredientText()
    {
        ingredientes.text = "" + PlayerClass.ingredients;
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        pauseScreen.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1;
    }

}
