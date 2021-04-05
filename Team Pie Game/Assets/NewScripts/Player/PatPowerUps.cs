using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatPowerUps : MonoBehaviour
{
    #region PowerUpEspeciais
    public void AumentarDuracaoEspecial(PowerUpButton button)
    {
        if (button.VerificarAtivacao() == 1)
        {
            PlayerPrefs.SetFloat("Especial+Duraçao", 1);
        }
        else if (button.VerificarAtivacao() == 2)
        {
            PlayerPrefs.SetFloat("Especial+Duraçao", 2);
        }
    }

    public void AumentarDanoEspecial(PowerUpButton button)
    {
        if (button.VerificarAtivacao() == 1)
        {
            PlayerPrefs.SetFloat("Especial+Dano", 0.15f);
        }
        else if (button.VerificarAtivacao() == 2)
        {
            PlayerPrefs.SetFloat("Especial+Dano", 0.3f);
        }
    }

    public void AumentarAreaEspecial(PowerUpButton button)
    {
        if (button.VerificarAtivacao() == 1)
        {
            PlayerPrefs.SetInt("Especial+Area", 1);
        }
        else if (button.VerificarAtivacao() == 2)
        {
            PlayerPrefs.SetInt("Especial+Area", 2);
        }
    }
    #endregion

    #region PowerUpSecundario
    public void DiminuirCoolDownSecudario(PowerUpButton button)
    {
        if (button.VerificarAtivacao() == 1)
        {
            PlayerPrefs.SetFloat("+Rapido", 2);
        }
        else if (button.VerificarAtivacao() == 2)
        {
            PlayerPrefs.SetFloat("+Rapido", 3);
        }
    }

    public void AumentarLentidaoSecundario(PowerUpButton button)
    {
        if (button.VerificarAtivacao() == 1)
        {
            PlayerPrefs.SetFloat("+SlowDown", 0.5f);
        }
        else if (button.VerificarAtivacao() == 2)
        {
            PlayerPrefs.SetFloat("+SlowDown", 1f);
        }
    }

    public void AumentaAreaDaPocaSecundario(PowerUpButton button)
    {
        if (button.VerificarAtivacao() == 1)
        {
            PlayerPrefs.SetInt("+AreaDePoça", 1);
        }
        else if (button.VerificarAtivacao() == 2)
        {
            PlayerPrefs.SetInt("+AreaDePoça", 2);
        }
    }
    #endregion

    #region PowerUpBasico
    public void AumentarAlcanceBasico(PowerUpButton button)
    {
        if (button.VerificarAtivacao() == 1)
        {
            PlayerPrefs.SetFloat("+Alcance", 0.1f);
        }
        else if (button.VerificarAtivacao() == 2)
        {
            PlayerPrefs.SetFloat("+Alcance", 0.2f);
        }
    }

    public void AumentarProjetilBasico(PowerUpButton button)
    {
        if (button.VerificarAtivacao() == 1)
        {
            PlayerPrefs.SetInt("Skill1Bullets", 1);
            PlayerPrefs.SetFloat("+Dano", 0.05f);
        }
        else if (button.VerificarAtivacao() == 2)
        {
            PlayerPrefs.SetInt("Skill1Bullets", 2);
            PlayerPrefs.SetFloat("+Dano", 0.1f);
        }
        
    }

    public void IndefinidoBasico(PowerUpButton button)//aumenta a porcentagem de chance de +50% de dano
    {
        if (button.VerificarAtivacao() == 1)
        {
            PlayerPrefs.SetInt("+ChanceDe+Dano", 5);
        }
        else if (button.VerificarAtivacao() == 2)
        {
            PlayerPrefs.SetInt("+ChanceDe+Dano", 8);
        }
    }
    #endregion
}
