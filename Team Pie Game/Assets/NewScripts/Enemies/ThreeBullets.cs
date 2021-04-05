using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeBullets : EnemyClass
{
    public GameObject bullet;
    public Transform[] shootPoints = new Transform[3];

    public float range;
    public float coolDown;

    private float realCoolDown;
    private bool canShoot;
    private bool isShooting;

    void Start()
    {
        EnemyStart();
        realCoolDown = coolDown;
    }

    
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        Vector3 dirToPlayer = (target.position - transform.position).normalized;
        if (!isShooting)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dirToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (distance > range)
        {
            SeguirAlvo();
            canShoot = false;
        }
        else
        {
            if (canShoot)
            {
                canShoot = false;
                realCoolDown = coolDown;
                Shoot();
            }
                
        }

        if(realCoolDown > 0)
        {
            realCoolDown -= Time.deltaTime;
        }
        else
        {
            canShoot = true;
        }
        
    }


    void Shoot()
    {
        isShooting = true;
        for (int i = 0; i < shootPoints.Length; i++)
        {
            Instantiate(bullet, shootPoints[i].position, shootPoints[i].rotation);
        }
        isShooting = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
