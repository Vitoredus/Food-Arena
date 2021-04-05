using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morteiro : EnemyClass
{
    public GameObject bullet;

    public Transform[] pontosNormais = new Transform[4];
    public Transform[] pontosVerticais = new Transform[4];

    public LayerMask layer;

    public Animator anim;

    void Start()
    {
        EnemyStart();
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            anim.SetTrigger("Attack");
            for (int i = 0; i < pontosNormais.Length; i++)
            {
                Instantiate(bullet, pontosNormais[i].position, pontosNormais[i].rotation);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < pontosVerticais.Length; i++)
            {
                Instantiate(bullet, pontosVerticais[i].position, pontosVerticais[i].rotation);
            }
            yield return new WaitForSeconds(3f);
        }
    }
    
}
