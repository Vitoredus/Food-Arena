using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabanete : EnemyClass
{
    public Transform shootPoint;
    public GameObject bulletPrefab;

    public float fleeRange;
    public float shootDelay;

    public ParticleSystem explosion;

    void Start()
    {
        EnemyStart();
        StartCoroutine(Shoot());
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        Vector3 dirToPlayer = transform.position - target.position;

        if (distance <= fleeRange)
        {
            Vector3 newPos = transform.position + dirToPlayer;
            agent.SetDestination(newPos);
        }

        Quaternion lookRotation = Quaternion.LookRotation(dirToPlayer);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            explosion.Play();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, fleeRange);
    }
}
