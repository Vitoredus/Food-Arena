using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touro : EnemyClass
{
    public Transform raycastPoint;
    public float range;
    public float raycastRange;

    private Vector3 playerPosition;

    private bool atacando = false;
    private bool parado = false;

    void Start()
    {
        EnemyStart();    
    }

    
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        Vector3 dirToPlayer = (target.position - transform.position).normalized;
        if (!atacando)
        {
            if (distance <= range)
            {
                Quaternion lookRotation = Quaternion.LookRotation(dirToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
            }
            else
            {
                SeguirAlvo();
            }

            RaycastHit hit;
            if (Physics.Raycast(raycastPoint.position, transform.forward, out hit, raycastRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    playerPosition = hit.transform.position;
                    atacando = true;
                    agent.enabled = false;
                }
            }
        }
        else
        {
            Attack();
        }
    }

    void Attack()
    {
        if(Vector3.Distance(transform.position, playerPosition) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPosition.x, transform.position.y, playerPosition.z), 5 * Time.deltaTime);
            //Debug.Log("se movendo");
        }
        else
        {
            transform.position.Normalize();
            if(!parado)
               StartCoroutine(Parar());
        }
    }

    IEnumerator Parar()
    {
        parado = true;
        yield return new WaitForSeconds(1f);
        atacando = false;
        parado = false;
        agent.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && atacando)
        {
            collision.gameObject.GetComponent<IKnockBack>().KnockBack(transform, 30);
            collision.gameObject.GetComponent<ILifeInterface>().TakeDamage(enemyObject.damage);
        }
    }
}
