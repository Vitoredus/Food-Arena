using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillClass : MonoBehaviour
{
    public static bool taAtacando;

    public Transform playerBody;
    [SerializeField] private Movimento playerMovement;

    public FixedJoystick joystick;
    public GameObject rangeIndicator;

    public GameObject projetil;
    public GameObject atkPoint;
    public GameObject atkPoint2;

    public Image barraRecarga;

    protected bool canShoot;
    protected bool atirando;
    private bool isAiming;

    protected float cooldown;
    public float tempoDeRecarga;

    public Animator playerAnimator;
    public Animator gunAnimator;

    public void RotacionarEAtacar(Transform transform, bool temRecarga = false, bool isUlt = false)
    {
        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            if (temRecarga) this.joystick.enabled = false;

            playerAnimator.SetBool("Atacar", false);
            gunAnimator.SetBool("Atacar", false);

            if (barraRecarga != null)
                barraRecarga.fillAmount += 1.0f / tempoDeRecarga * Time.deltaTime;

            return;
        }
        else
        {
            if (joystick.enabled == false)
                this.joystick.enabled = true;
        }

        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            taAtacando = true;
            playerAnimator.SetBool("Atacar", true);
            gunAnimator.SetBool("Atacar", true);
            playerMovement.canMove = false;
            playerMovement.atacando = true;
            var angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;

            if (isUlt)
            {
                transform.LookAt(new Vector3(rangeIndicator.transform.position.x, playerBody.position.y, rangeIndicator.transform.position.z));
                playerBody.LookAt(new Vector3(rangeIndicator.transform.position.x, playerBody.position.y, rangeIndicator.transform.position.z));
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, angle, 0);
                playerBody.rotation = Quaternion.Euler(0, angle, 0);
            }

            rangeIndicator.SetActive(true);
            isAiming = true;

            if (isAiming == true && cooldown <= 0)
            {
                atkPoint2.transform.position = atkPoint.transform.position;
                atkPoint2.transform.rotation = atkPoint.transform.rotation;
                canShoot = true;
            }

        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0 && canShoot == true && taAtacando)
        {
            taAtacando = false;
            playerAnimator.SetBool("Atacar", false);
            gunAnimator.SetBool("Atacar", false);
            playerMovement.canMove = true;
            playerMovement.atacando = false;
            atirando = true;
            isAiming = false;
            rangeIndicator.SetActive(false);
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0 && canShoot == false && !taAtacando)
        {
            playerMovement.canMove = true;
            playerMovement.atacando = false;
            rangeIndicator.SetActive(false);
        }

       
    }

    public virtual void GenericShoot()
    {
        canShoot = false;
        cooldown = tempoDeRecarga;
    }

    protected void ZerarRecarga()
    {
        barraRecarga.fillAmount = 0;
    }
}

