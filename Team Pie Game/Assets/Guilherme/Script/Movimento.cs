using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    public float velocidade;
    public FixedJoystick joystick;

    public Transform body;

    private float h;
    private float v;

    [HideInInspector] public bool canMove;
    [HideInInspector] public bool atacando;
    public Animator playerAnimator;
    public Animator gunAnimator;

    void FixedUpdate()
    {
        h = joystick.Horizontal;
        v = joystick.Vertical;

        if (h != 0 && v != 0)
        {
            transform.Translate(new Vector3(Mathf.Clamp(h, -0.1f, 0.1f), 0, Mathf.Clamp(v, -0.1f, 0.1f)) * velocidade * Time.deltaTime);

            if(!atacando) playerAnimator.SetBool("Correr", true);
            if (!atacando) playerAnimator.SetBool("Idle", false);
            if (!atacando) gunAnimator.SetBool("Correr", true);
            if (!atacando) gunAnimator.SetBool("Idle", false);
        }
        else
        {
            if (!atacando) playerAnimator.SetBool("Correr", false);
            if (!atacando) playerAnimator.SetBool("Idle", true);
            if (!atacando) gunAnimator.SetBool("Correr", false);
            if (!atacando) gunAnimator.SetBool("Idle", true);
        }
        
        Rotacionar();
    }

    void Rotacionar()
    {
        if (h != 0 && v != 0 && canMove)
        {
            var angle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
            body.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
  
}
