using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    private static bool popUpAberto;

    [SerializeField] private Image backImage;

    [Header("Caso o botão seja dependente de outro")]
    [SerializeField] private bool temDependencia;
    [SerializeField] PowerUpButton dependencia;

    [Header("Configuração")]
    [SerializeField] private GameObject popUpScreen;

    [SerializeField] private Button acceptButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Text descriptionText;

    [SerializeField]private string nome;
    [TextArea(3,10)]
    [SerializeField]private string[] description;

    public int custo;
    public UnityEvent functionToActivate;

    [Header("Caso este botao tenha dependentes")]
    public UnityEvent desbloquearBotoesDependentes;

    private int foiAtivada = 0;

    private bool podeAbrir = true;

    private void Start()
    {
        if (PlayerPrefs.HasKey(nome))
        {
            this.foiAtivada = PlayerPrefs.GetInt(nome);

            if (foiAtivada == 2)
            {
                backImage.fillAmount = 1;
                podeAbrir = false;
            }
            else
            {
                backImage.fillAmount = 0.5f;
            }

            if(foiAtivada > 0)
            {
                this.desbloquearBotoesDependentes?.Invoke();
            }
        }

        if(temDependencia)
        {
            if (dependencia.VerificarAtivacao() == 0)
            {
                image.color = Color.gray;
                podeAbrir = false;
            }
        }
    }

    public void TornarInteragivel()
    {
        if(foiAtivada != 2)
        {
            //this.button.interactable = true;
            image.color = Color.white;
            podeAbrir = true;
        }
    }

    public void AbrirPopUp()
    {
        if (!popUpAberto && podeAbrir)
        {
            popUpAberto = true;
            popUpScreen.SetActive(true);
            descriptionText.text = description[foiAtivada];
            acceptButton.onClick.AddListener(AtivarHabilidade);
            cancelButton.onClick.AddListener(FecharPopUp);
        }
    }

    public void FecharPopUp()
    {
        acceptButton.onClick.RemoveListener(AtivarHabilidade);
        cancelButton.onClick.RemoveListener(FecharPopUp);
        popUpScreen.SetActive(false);
        popUpAberto = false;
    }

    private void AtivarHabilidade()
    {
        if (SettingsMenu.Instance.selectedCharacter.experiencePoints >= custo)
        {
            SettingsMenu.Instance.selectedCharacter.experiencePoints -= custo;
            SettingsMenu.Instance.UpdtadeUI();
            foiAtivada++;
            backImage.fillAmount = 0.5f;

            if (foiAtivada == 2)
            {
                backImage.fillAmount = 1;
                //image.color = Color.gray;
                podeAbrir = false;
                
            }

            
            FecharPopUp();
            functionToActivate?.Invoke();

            this.desbloquearBotoesDependentes?.Invoke();
            SaveButtonState();
        }
    }


    void SaveButtonState()
    {
        PlayerPrefs.SetInt(nome, foiAtivada);
    }

    public int VerificarAtivacao()
    {
        return foiAtivada;
    }

}
