using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Image timer;
    public ExpScreen expScreen;

    private void OnEnable()
    {
        timer.fillAmount = 0;
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        float countdown = 0;
        while (countdown <=5)
        {
            countdown += Time.deltaTime;
            timer.fillAmount = countdown/5;
            yield return null;
        }
        UIManager.instance.ActiveExpScreen();
    }

}
