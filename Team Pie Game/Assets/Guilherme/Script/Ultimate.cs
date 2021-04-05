using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ultimate : MonoBehaviour
{
    public FixedJoystick joystick;
    public GameObject mira;
    public bool canShoot;
    public bool isAiming;
    private float cooldown;
    public float tempoDeRecarga;

    public GameObject projetil;
    public GameObject ponto;
    public GameObject ponto2;

    public Image barraRecarga;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            var angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
            mira.SetActive(true);
            isAiming = true;

            if (isAiming == true && cooldown <= 0)
            {
                ponto2.transform.position = ponto.transform.position;
                ponto2.transform.rotation = ponto.transform.rotation;
                canShoot = true;


            }

        }
        if (joystick.Horizontal == 0 && joystick.Vertical == 0 && canShoot == true)
        {
            StartCoroutine(Shoot());
            isAiming = false;
            mira.SetActive(false);

        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0 && canShoot == false)
        {
            mira.SetActive(false);
        }

        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            barraRecarga.fillAmount += 1.0f / tempoDeRecarga * Time.deltaTime;
        }

    }

    IEnumerator Shoot()
    {
        canShoot = false;
        cooldown = tempoDeRecarga;

        Instantiate(projetil, ponto2.transform.position, ponto2.transform.rotation);
        barraRecarga.fillAmount = 0;
        yield return new WaitForSeconds(1f);
    }


}
