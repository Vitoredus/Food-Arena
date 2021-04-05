using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    public Animator AM;
    public void IrParaJogo(string cena)
    {
        StartCoroutine(LoadScene(cena));
        
    }
    IEnumerator LoadScene(string cena)
    {
        AM.SetTrigger("Ficar grande");
        yield return new WaitForSeconds(1.5f);
        HelpfulFunctions.LoadScene(cena);
    }

}
